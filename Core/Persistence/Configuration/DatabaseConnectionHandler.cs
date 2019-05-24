using System;
using System.Data;
using Npgsql;

namespace Core.Persistence.Configuration
{
    public interface IDatabaseConnectionString
    {
        string ConnectionString { get; }
    }
    
    public class DatabaseConnectionStringProvider : IDatabaseConnectionString
    {
        private static string _connectionString;

        public string ConnectionString => GetConnectionString();
        
        public static string GetConnectionString()
        {
            if (_connectionString != null) return _connectionString;

            _connectionString = GetConnectionStringFromEnvironment() ?? DefaultConnectionString;
            return _connectionString;
        }
        
        private static string GetConnectionStringFromEnvironment()
        {
            return Environment.GetEnvironmentVariable("DB_CONNECTION");
        }

        private static string DefaultConnectionString => "Server=127.0.0.1;Port=5432;Database=asg;User Id=asgwebapp;Password=admin;";
    }
    
    public class DatabaseConnection : IDisposable
    {
        private NpgsqlConnection _con;
        public NpgsqlConnection Db
        {
            get
            {
                if (CanOpen)
                {
                    _con.Open();
                }

                return _con;
            }
        }

        private bool CanOpen => _con.FullState == ConnectionState.Closed;

        public DatabaseConnection(IDatabaseConnectionString connectionString)
        {
            _con = new NpgsqlConnection(connectionString.ConnectionString);
        }

        public void Dispose()
        {
            if (!CanOpen)
            {
                _con.Close();
            }

            _con.Dispose();
            _con = null;
        }
    }
}