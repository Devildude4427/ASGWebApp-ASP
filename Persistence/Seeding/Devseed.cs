using System.Threading.Tasks;
using Autofac;
using Dapper;
using Persistence.Configuration;

namespace Persistence.Seeding
{
    public class Devseed
    {
        public static async Task Execute(IContainer container)
        {
            var connection = container.Resolve<DatabaseConnection>();

            await connection.Db.ExecuteAsync(@"
                INSERT INTO users(name, email, role, password_hash, activated)
                VALUES('Admin User', 'admin@example.com', 9001, '$2y$12$sx43vQhbkljyyofORuxz8.4GMc4kysuwroFl8.5pJQGfRhrun1Cwm', true);
            ");
        }
    }
}