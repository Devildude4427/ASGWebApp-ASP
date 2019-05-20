using System;
using Npgsql;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace Core.Persistence.Configuration
{
    public class DatabaseInitializer : IDisposable
    {
        private readonly NpgsqlConnection _con;

        public DatabaseInitializer(string connectionString)
        {
            _con = new NpgsqlConnection(connectionString);
            _con.Open();
        }

        public void InitializeDatabase()
        {
            var migrator = new SimpleMigrator(typeof(DatabaseInitializer).Assembly,
                new PostgresqlDatabaseProvider(_con));
            migrator.Load();
            migrator.MigrateTo(1);
            migrator.MigrateToLatest();
        }
        
        public void DropDatabase()
        {
            using (var transaction = _con.BeginTransaction())
            {
                var command = _con.CreateCommand();
                command.CommandText = @"
                    DO $$ DECLARE
                        r RECORD;
                    BEGIN
                        FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = current_schema()) LOOP
                            EXECUTE 'DROP TABLE IF EXISTS ' || quote_ident(r.tablename) || ' CASCADE';
                        END LOOP;
                    END $$;
                ";

                command.ExecuteNonQuery();

                transaction.Commit();
            }
        }
        
        public void Dispose()
        {
            _con.Close();
            _con.Dispose();
        }
    }
}