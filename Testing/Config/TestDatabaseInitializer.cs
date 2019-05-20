using System;
using System.Threading.Tasks;
using Autofac;
using Core.Persistence.Configuration;
using Core.Persistence.Seeding;

namespace Testing.Config
{
    public class TestDatabaseInitializer : IDisposable
    {
        public TestDatabaseInitializer(IContainer container)
        {
            using (var dbInitializer = new DatabaseInitializer(DatabaseConnectionStringProvider.GetConnectionString()))
            {
                Console.WriteLine("Dropping database objects");
                dbInitializer.DropDatabase();
                Console.WriteLine("Creating database objects");
                dbInitializer.InitializeDatabase();
                Console.WriteLine();
                Task.Run(async () => await Testseed.Execute(container)).Wait();
                Console.WriteLine("Database ready");
            }
        }
        
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }
        
        public DatabaseConnection GetConnection()
        {
            return new DatabaseConnection(new DatabaseConnectionStringProvider());
        }
    }
}