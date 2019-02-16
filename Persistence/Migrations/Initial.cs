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
                  id BIGSERIAL PRIMARY KEY,
                  name text NOT NULL,
                  email text NOT NULL,
                  role BIGINT NOT NULL,
                  password_hash text,
                  activated BOOLEAN NOT NULL DEFAULT FALSE,
                  activation_token TEXT,
                  disabled BOOLEAN NOT NULL DEFAULT FALSE,
                  CONSTRAINT unique_email UNIQUE(email)
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