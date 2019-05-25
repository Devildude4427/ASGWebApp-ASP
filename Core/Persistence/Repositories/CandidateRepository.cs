using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Models.Course;
using Core.Domain.ViewModels;
using Core.Persistence.Configuration;
using Dapper;

namespace Core.Persistence.Repositories
{
    public interface ICandidateRepository
    {
        Task<PaginatedList<User>> Find(FilteredPageRequest filteredPageRequest);
        Task<IEnumerable<Candidate>> GetAll();
        Task<Candidate> FindByUserId(long id);
        Task<bool> CheckExists(string email);
        Task<string> PreviousCandidateReferenceNumber();
        Task<bool> Register(CommercialRegistrationRequest commercialRegistration);
        Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails);
    }
    
    class CandidateRepository : ICandidateRepository
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

        public async Task<IEnumerable<Candidate>> GetAll()
        {
            const string sql = @"
                SELECT u.name, c.reference_number, gi.preferred_course_location, c.last_completed_stage
                FROM candidates c
                INNER JOIN users u on c.user_id = u.id
                INNER JOIN general_information gi on c.general_info_id = gi.id
                ";
            var result = await _con.Db.QueryMultipleAsync(sql);
            return await result.ReadAsync<Candidate>();
        }

        public async Task<Candidate> FindByUserId(long id)
        {
            const string sql = @"
                SELECT *
                FROM candidates c
                WHERE c.user_id = :id;
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<Candidate>(sql, new { id });
        }
        
        public async Task<bool> CheckExists(string email)
        {
            const string sql = @"
                SELECT exists(
                    SELECT 1 FROM candidates c
                    INNER JOIN users u on c.user_id = u.id 
                    WHERE U.email = :email
                );
            ";
            return await _con.Db.QuerySingleOrDefaultAsync<bool>(sql, new { email });
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
        
        public async Task<bool> Register(CommercialRegistrationRequest courseRegistration)
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
                courseRegistration.ReferenceNumber, contactInformationId, generalInformationId, DateTime.Now});
            return rowsAffected == 1;
        }
        
        public async Task<bool> UpdateDetails(UpdateContactDetails updateContactDetails)
        {
             string sql;
             var rowsAffected = 0;

             if (updateContactDetails.Address != null)
             {
                 sql = @"
                    UPDATE addresses AS a
                    SET line_1 = :Line1, line_2 = :Line2, city = :City, post_code = :PostCode
                    FROM candidates AS c, contact_information AS ci
                    WHERE c.user_id = :UserId
                    AND c.contact_info_id = ci.id
                    AND ci.address_id = a.id;
                ";
                 rowsAffected += await _con.Db.ExecuteAsync(sql, new {
                     updateContactDetails.Address.Line1, updateContactDetails.Address.Line2, updateContactDetails.Address.City, 
                     updateContactDetails.Address.PostCode, updateContactDetails.UserId });
             }

             if (updateContactDetails.PhoneNumber != null)
             {
                 sql = @"
                    UPDATE contact_information AS ci
                    SET phone_number = :PhoneNumber
                    FROM candidates AS c
                    WHERE c.user_id = :UserId
                    AND c.contact_info_id = ci.id;
                ";
                 rowsAffected += await _con.Db.ExecuteAsync(sql, new {
                     updateContactDetails.PhoneNumber, updateContactDetails.UserId });
             }

             if (updateContactDetails.Email == null) return rowsAffected > 1;
             sql = @"
                    UPDATE users AS u
                    SET email = :Email
                    FROM candidates AS c
                    WHERE u.id = :UserId
                ";
             rowsAffected += await _con.Db.ExecuteAsync(sql, new {
                 updateContactDetails.Email, updateContactDetails.UserId });

             return rowsAffected > 1;
        }
    }
}