using System;
using System.Threading.Tasks;
using Autofac;
using Persistence.Configuration;
using Persistence.Seeding;

namespace Tests.Integration.Config
{
    public class TestDatabaseInitialiser : IDisposable
    {
        public TestDatabaseInitialiser(IContainer container)
        {
            using (var dbInitialiser = new DatabaseInitialiser(DatabaseConnectionStringProvider.GetConnectionString()))
            {
                Console.WriteLine("Dropping database objects");
                dbInitialiser.DropDatabase();
                Console.WriteLine("Creating database objects");
                dbInitialiser.InitialiseDatabase();
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