using SimpleMigrations;

namespace Persistence.Migrations
{
    [Migration(2, "Instructors")]
    public class V2Instructors : Migration
    {
        protected override void Up()
        {
            Execute(@"
                CREATE TABLE instructors (
                  id SERIAL PRIMARY KEY,
                  user_id SERIAL NOT NULL REFERENCES users(id),
                  current_class TEXT
                );
            ");
        }

        protected override void Down()
        {
            Execute(@"
                DROP TABLE instructors;
            ");
        }
    }
    
}