using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ViewModels;
using Persistence.Configuration;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _con;

        public UserRepository(DatabaseConnection con)
        {
            _con = con;
        }
        
        //CRUD

        public async Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest)
        {
            var sqlTemplate = @"
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

        public async Task<User> FindById(long id)
        {
            var sql = @"
                SELECT *
                FROM users u
                WHERE u.id = :id;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<User>(sql, new { id });
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

        public async Task<bool> Register(RegistrationRequest registrationRequest)
        {
            const string sql = @"
                INSERT INTO users(name, email, password, role, activated, enabled, authentication_token, expiry_datetime)
                 VALUES (:Name, :EmailAddress, :Password, 1001, FALSE, TRUE, NULL, NULL) RETURNING id;
            ";
            var rowsAffected = await _con.Db.ExecuteAsync(sql, new {registrationRequest.Name,
                registrationRequest.Email, registrationRequest.Password});
            return rowsAffected == 1;
        }

        //Other
        public async Task<string> GetHashedPassword(string email)
        {
            var sql = @"
                SELECT password
                FROM users u
                WHERE u.email = LOWER(:email);
            ";
            return await _con.Db.QuerySingleAsync<string>(sql, new { email });
        }
    }
}