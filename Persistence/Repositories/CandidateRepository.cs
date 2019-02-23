using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Persistence.Configuration;

namespace Persistence.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly DatabaseConnection _con;

        public CandidateRepository(DatabaseConnection con)
        {
            _con = con;
        }
        
        //CRUD

        public async Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest)
        {
            const string sqlTemplate = @"
                SELECT *
                FROM users u
                WHERE u.name ILIKE :searchTerm
                {0}
                OFFSET :offset
                LIMIT :pageSize;
                
                SELECT COUNT(*)
                FROM users u
                WHERE u.name ILIKE :searchTerm
                OFFSET :offset
                LIMIT :pageSize;
            ";
            
            var sanitisedSql = new SanitisedSql<User>(sqlTemplate, filteredPageRequest.OrderBy, filteredPageRequest.OrderByAsc, "u");

            var sql = sanitisedSql.ToSql();

            var result = await _con.Db.QueryMultipleAsync(sql, new
            {
                filteredPageRequest.SearchTerm,
                offset = (int) filteredPageRequest.Offset,
                pageSize = (int) filteredPageRequest.PageSize
            });

            var users = await result.ReadAsync<User>();
            var count = await result.ReadSingleAsync<long>();
            
            return new PaginatedList<User>(users.ToList(), count, filteredPageRequest);
        }

        public async Task<Candidate> FindByUserId(long id)
        {
            const string sql = @"
                SELECT *
                FROM candidates u
                WHERE u.user_id = :id;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<Candidate>(sql, new { id });
        }

        public async Task<User> FindByEmail(string email)
        {
            const string sql = @"
                SELECT *
                FROM users u
                WHERE u.email = :email;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<User>(sql, new { email });
        }
    }
}