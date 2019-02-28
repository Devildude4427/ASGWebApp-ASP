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
                  user_id SERIAL NOT NULL,
                  reference_number TEXT NOT NULL,
                  contact_info_id SERIAL NOT NULL,
                  general_info_id SERIAL NOT NULL,
                  last_completed_stage INTEGER NOT NULL,
                  CONSTRAINT unique_reference_number UNIQUE(reference_number)
                );
            ");
            
            Execute(@"
                CREATE TABLE address (
                  id SERIAL PRIMARY KEY,
                  line_1 TEXT NOT NULL,
                  line_2 TEXT NOT NULL,
                  city TEXT NOT NULL,
                  post_code TEXT NOT NULL
                );
            ");
            
            Execute(@"
                CREATE TABLE contact_information (
                  id SERIAL PRIMARY KEY,
                  address_id Serial NOT NULL,
                  phone_number TEXT NOT NULL
                );
            ");
            
            Execute(@"
                CREATE TABLE drones (
                  id SERIAL PRIMARY KEY,
                  make TEXT NOT NULL,
                  model TEXT NOT NULL
                );
            ");
            
            Execute(@"
                CREATE TABLE general_information (
                  id SERIAL PRIMARY KEY,
                  english_speaking_level INTEGER NOT NULL,
                  disability TEXT,
                  place_of_birth TEXT NOT NULL,
                  date_of_birth DATE NOT NULL,
                  company_name TEXT,
                  flight_experience TEXT,
                  preferred_course_location TEXT NOT NULL,
                  drone_id SERIAL NOT NULL,
                  paid BOOLEAN NOT NULL
                );
            ");
        }

        protected override void Down()
        {
            Execute(@"
                DROP TABLE users;
                DROP TABLE candidates;
                DROP TABLE address;
                DROP TABLE contact_information;
                DROP TABLE drones;
                DROP TABLE general_information;
            ");
        }
    }
}