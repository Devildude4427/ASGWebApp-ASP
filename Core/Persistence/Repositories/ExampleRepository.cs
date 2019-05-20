using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Core.Persistence.Configuration;
using Dapper;

namespace Core.Persistence.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly DatabaseConnection _con;

        public ExampleRepository(DatabaseConnection con)
        {
            _con = con;
        }
        
        public async Task<bool> Create(Example newEntity)
        {
            var sql = @"
                INSERT INTO example(name)
                VALUES(:name);
            ";

            var rowsAffected = await _con.Db.ExecuteAsync(sql, newEntity);
            return rowsAffected == 1;
        }
        
        public async Task<PaginatedList<Example>> Find(FilteredPageRequest pageRequest)
        {
            const string sqlTemplate = @"SELECT * FROM users OFFSET :offset LIMIT :pageSize;
                                SELECT COUNT(*) FROM users;";
            
            // var sqlTemplate = @"
            //     SELECT *
            //     FROM example e
            //     WHERE e.name ILIKE :searchTerm
            //     {0}
            //     OFFSET :offset
            //     LIMIT :pageSize;
            //
            //     SELECT COUNT(*)
            //     FROM example e
            //     WHERE e.name ILIKE :searchTerm;
            // ";
            
            var sanitisedSql = new SanitisedSql<Example>(sqlTemplate, pageRequest.OrderBy, pageRequest.OrderByAsc, "p");

            var sql = sanitisedSql.ToSql();
            
            var result = await _con.Db.QueryMultipleAsync(sql, new
            {
                pageRequest.SearchTerm,
                offset = (int) pageRequest.Offset,
                pageSize = (int) pageRequest.PageSize
            });

            var examples = await result.ReadAsync<Example>();
            var count = await result.ReadSingleAsync<long>();
            
            return new PaginatedList<Example>(examples.ToList(), count, pageRequest);
        }
        
        public async Task<Example> FindById(long id)
        {
            var sql = @"
                SELECT *
                FROM example e
                WHERE e.id = :id;
            ";

            var result = await _con.Db.QuerySingleOrDefaultAsync<Example>(sql, new { id });
            return result;
        }

        public async Task<bool> Update(long id, Example updatedEntity)
        {
            var sql = @"
                UPDATE example
                SET name = :name
                WHERE id = :id;
            ";

            var rowsAffected = await _con.Db.ExecuteAsync(sql, new
            {
                updatedEntity.Name,
                id
            });

            return rowsAffected == 1;
        }
        
        public async Task<bool> Delete(long id)
        {
            var sql = @"
                DELETE FROM example
                WHERE id = :id;
            ";

           var rowsAffected = await _con.Db.ExecuteAsync(sql, new { id });
           return rowsAffected == 1;
        }
    }
}