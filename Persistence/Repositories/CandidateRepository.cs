using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ViewModels;
using Persistence.Configuration;

namespace Persistence.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly DatabaseConnection _con;

        public CandidateRepository(DatabaseConnection con)
        {
            _con = con;
        }
        
        //CRUD

        public async Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest)
        {
            const string sqlTemplate = @"
                SELECT *
                FROM users u
                WHERE u.name ILIKE :searchTerm
                
                OFFSET :offset
                LIMIT :pageSize;
                
                SELECT COUNT(*)
                FROM users u
                WHERE u.name ILIKE :searchTerm
                OFFSET :offset
                LIMIT :pageSize;
            ";
            
            var sanitisedSql = new SanitisedSql<User>(sqlTemplate, filteredPageRequest.OrderBy, filteredPageRequest.OrderByAsc, "u");

            var sql = sanitisedSql.ToSql();

            var result = await _con.Db.QueryMultipleAsync(sql, new
            {
                filteredPageRequest.SearchTerm,
                offset = (int) filteredPageRequest.Offset,
                pageSize = (int) filteredPageRequest.PageSize
            });

            var users = await result.ReadAsync<User>();
            var count = await result.ReadSingleAsync<long>();
            
            return new PaginatedList<User>(users.ToList(), count, filteredPageRequest);
        }

        public async Task<Candidate> FindByUserId(long id)
        {
            const string sql = @"
                SELECT *
                FROM candidates u
                WHERE u.user_id = :id;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<Candidate>(sql, new { id });
        }

        public async Task<int> GetCurrentCandidateCount()
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM  candidates;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<int>(sql);
        }

        public async Task<string> PreviousCandidateReferenceNumber()
        {
            const string sql = @"
                SELECT reference_number
                FROM candidates 
                ORDER BY id DESC 
                LIMIT 1
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<string>(sql);
        }
        
        public async Task<bool> Register(CourseRegistration courseRegistration)
        {
            var sql = @"
                INSERT INTO addresses(line_1, line_2, city, post_code) VALUES (:Line1, :Line2, :City, :PostCode) RETURNING id;
            ";
            var addressId = _con.Db.Query<int>(sql, courseRegistration.Address).Single();
            
            sql = @"
                INSERT INTO contact_information(address_id, phone_number) VALUES (:AddressId, :PhoneNumber) RETURNING id;
            ";
            var contactInformationId = _con.Db.Query<int>(sql, new { AddressId = addressId, courseRegistration.PhoneNumber}).Single();
            
            
            sql = @"
                INSERT INTO drones(make, model) VALUES (:Make, :Model) RETURNING id;
            ";
            var droneId = _con.Db.Query<int>(sql, courseRegistration.Drone).Single();
            
            sql = @"
                INSERT INTO general_information(english_speaking_level, disability, place_of_birth, date_of_birth,
                                                company_name, flight_experience, preferred_course_location, drone_id, paid)
                                                 VALUES (:EnglishSpeakingLevel, :Disability, :PlaceOfBirth, :DateOfBirth,
                                                         :CompanyName, :FlightExperience, :PreferredCourseLocation,
                                                         :DroneId, true) RETURNING id;
            ";
            var generalInformationId = _con.Db.Query<int>(sql, new { courseRegistration.EnglishSpeakingLevel,
                courseRegistration.Disability, courseRegistration.PlaceOfBirth, courseRegistration.DateOfBirth,
                courseRegistration.CompanyName, courseRegistration.FlightExperience, courseRegistration.PreferredCourseLocation,
                DroneId = droneId}).Single();
            
            sql = @"
                INSERT INTO candidates(user_id, reference_number, contact_info_id, general_info_id, last_completed_stage, registration_date)
                 VALUES (:UserId, :ReferenceNumber, :ContactInfoId, :GeneralInfoId, 0, :CurrentDate) RETURNING id;
            ";
            var rowsAffected = await _con.Db.ExecuteAsync(sql, new {courseRegistration.UserId,
                courseRegistration.ReferenceNumber, ContactInfoId = contactInformationId, GeneralInfoId = generalInformationId, CurrentDate = DateTime.Now});
            Console.WriteLine("Rows affected: " + rowsAffected);
            return rowsAffected == 1;
        }
        
        public async Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails)
        {
            //TODO Figure out how to handle the potentially blank fields and complex update
            // const string sql = @"
            //     UPDATE addresses
            //     SET line_1 = :Line1, line_2 = :Line2, city = :City, post_code = :PostCode
            //     FROM contact_information AS c
            //     WHERE v.shipment_id = s.id 
            // ";
            // var rowsAffected = await _con.Db.ExecuteAsync(sql, new {courseRegistration.UserId,
            //     courseRegistration.ReferenceNumber, ContactInfoId = contactInformationId, GeneralInfoId = generalInformationId});
            // Console.WriteLine("Rows affected: " + rowsAffected);
            // return rowsAffected == 1;
            return true;
        }
    }
}