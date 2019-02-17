using System.Threading.Tasks;
using Autofac;
using Dapper;
using Persistence.Configuration;

namespace Persistence.Seeding
{
    public static class Devseed
    {
        public static async Task Execute(IContainer container)
        {
            var connection = container.Resolve<DatabaseConnection>();

            await connection.Db.ExecuteAsync(@"
                INSERT INTO users(first_name, last_name, email, role, password, activated)
                VALUES('Erwin', 'Schroedinger', 'admin@asg.com', '9001', '$2y$12$sx43vQhbkljyyofORuxz8.4GMc4kysuwroFl8.5pJQGfRhrun1Cwm', true),
                      ('Johannes', 'Kepler', 'admin2@asg.com', '9001', '$2a$10$cGXMmSfOg2ZrUXUgCxaiaO4pgjJsSvcFYlKPMcFDBgVp3MqrZ2M96', true),
                      ('Blaise', 'Pascal', 'candidate@asg.com', '1001', '$2a$10$S4MOcUgShERWJpI1EpTReeNbwKL09wElxbCLLimHdf3yrOG7H2PWG', true),
                      ('Caroline', 'Herschel', 'candidate2@asg.com', '1001', '$2a$10$uzGe/I2v.1LtdTPBQg8oDOe/07mlgaGyG0UoF8G5VQD7b6PAvTQHq', true),
                      ('Edmond', 'Halley', 'candidate3@asg.com', '1001', '$2a$10$H2GWvDnlRJD1DuVPkfkSLujsmzSwGmM7ELKpZY0olKQFObp4GWZ56', true),
                      ('Enrico', 'Fermi', 'candidate4@asg.com', '1001', '$2a$10$1XELQEuTrZ0JbFg0MijjI.4QFWJYMppieYp5XN8Aj6z6LIg9QwgN2', true);
            ");
        }
    }
}