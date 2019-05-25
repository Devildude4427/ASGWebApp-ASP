using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.Models.Authentication;
using Core.Persistence.Configuration;
using Dapper;

namespace Core.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindById(long id);
        Task<User> FindByEmail(string email);
        Task<bool> Register(RegistrationRequest registrationRequest);
        Task<string> GetHashedPassword(string email);
    }
    
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConnection _con;

        public UserRepository(DatabaseConnection con)
        {
            _con = con;
        }

        public async Task<User> FindById(long id)
        {
            const string sql = @"
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
                 VALUES (:Name, :Email, :Password, 1001, FALSE, TRUE, NULL, NULL) RETURNING id;
            ";
            var rowsAffected = await _con.Db.ExecuteAsync(sql, new {
                registrationRequest.Name, registrationRequest.Email, registrationRequest.Password});
            return rowsAffected == 1;
        }

        public async Task<string> GetHashedPassword(string email)
        {
            const string sql = @"
                SELECT password
                FROM users u
                WHERE u.email = LOWER(:email);
            ";
            return await _con.Db.QuerySingleAsync<string>(sql, new { email });
        }
    }
}