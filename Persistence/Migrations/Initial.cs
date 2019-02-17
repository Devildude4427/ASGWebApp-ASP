using SimpleMigrations;

namespace Persistence.Migrations
{
    [Migration(1, "Initial")]
    public class Initial : Migration
    {
        protected override void Up()
        {
            Execute(@"
                CREATE TABLE users (
                  id SERIAL PRIMARY KEY,
                  name TEXT NOT NULL,
                  email TEXT NOT NULL,
                  password TEXT NOT NULL,
                  role INTEGER NOT NULL,
                  activated BOOLEAN NOT NULL DEFAULT FALSE,
                  enabled BOOLEAN NOT NULL DEFAULT TRUE,
                  authentication_token TEXT,
                  expiry_datetime TIMESTAMP,
                  CONSTRAINT unique_email UNIQUE(email)
                );
            ");
            
            Execute(@"
                CREATE TABLE candidates (
                  id SERIAL PRIMARY KEY,
                  user_id INTEGER NOT NULL,
                  reference_number TEXT NOT NULL,
                  contact_info_id INTEGER NOT NULL,
                  general_info_id INTEGER NOT NULL,
                  CONSTRAINT unique_reference_number UNIQUE(reference_number)
                );
            ");
        }

        protected override void Down()
        {
            Execute(@"
                DROP TABLE users;
            ");
        }
    }
}