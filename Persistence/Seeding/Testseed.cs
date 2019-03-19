using System.Threading.Tasks;
using Autofac;
using Dapper;
using Persistence.Configuration;

namespace Persistence.Seeding
{
    public class Testseed
    {
        public static async Task Execute(IContainer container)
        {
            var connection = container.Resolve<DatabaseConnection>();

            await connection.Db.ExecuteAsync(@"
                INSERT INTO users(name, email, role, password, activated)
                VALUES ('Erwin Schroedinger', 'admin@asg.com', '9001', '$2y$12$IF458i6pDVsY.XJR5OYd.OzjRPaPIsHmQhyE93E2V2SonZszVmzXm', true),
                       ('Edmond Halley', 'candidate3@asg.com', '1001', '$2y$12$E9LNbS1h2VZ/lErbbWunVOpA67QJrPmPCqCBuGuLKuMY/r.QjEjea', true),
                       ('Enrico Fermi', 'candidate4@asg.com', '1001', '$2y$12$YGA1lrJ7MlhgKtNU0qy4UOtrvlYpswFlMjVrqDdaFQLTS54/MYhyy', true);

                INSERT INTO addresses(line_1, line_2, city, post_code)
                VALUES ('Box 777, 91 Western Road', 'Brighton', 'East Sussex', 'BN1 2NW'),
                       ('Room 67, 14 Tottenham Court Road', '', 'London', 'W1T 1JY');

                INSERT INTO contact_information (address_id, phone_number)
                VALUES (1, '7911123456'),
                       (2, '7911564856');

                INSERT INTO drones (make, model)
                VALUES ('DJI', 'Matrice'),
                       ('DJI', 'Mavic'),
                       ('DJI', 'Mavic 2'),
                       ('DJI', 'Mavic Air'),
                       ('DJI', 'Mavic Pro');

                INSERT INTO general_information(english_speaking_level, place_of_birth, date_of_birth, preferred_course_location, drone_id, paid)
                VALUES (6, 'Aberdeenshire', '1990-12-31', 'Aberdeenshire', 1, true),
                       (6, 'Cardiff', '1990-11-15', 'Cardiff', 5, true);

                INSERT INTO candidates(user_id, reference_number, contact_info_id, general_info_id, last_completed_stage, registration_date)
                VALUES (2, 'ASG-003-19-02', 1, 1, 7, '2019-03-19'),
                       (3, 'ASG-004-19-02', 2, 2, 12, '2019-03-16');
            ");
        }
    }
}