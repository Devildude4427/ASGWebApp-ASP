using System.Threading.Tasks;
using Autofac;
using Dapper;
using Persistence.Configuration;

namespace Persistence.Seeding
{
    public static class Devseed
    {
        public static async Task Execute(IContainer container)
        {
            var connection = container.Resolve<DatabaseConnection>();

            await connection.Db.ExecuteAsync(@"
                INSERT INTO users(name, email, role, password, activated)
                VALUES ('Erwin Schroedinger', 'admin@asg.com', '9001', '$2y$12$IF458i6pDVsY.XJR5OYd.OzjRPaPIsHmQhyE93E2V2SonZszVmzXm', true),
                       ('Johannes Kepler', 'admin2@asg.com', '9001', '$2y$12$D0lwJAXsCv8IzGfex0wV7uGdKj1nuYweTc6BnMZbS8xss/djGmySO', true),
                       ('Blaise Pascal', 'candidate@asg.com', '1001', '$2y$12$OZ6iLbvvFeMAPh.w8.ZLFetakRl9B5gA6MvCHgJs9kK2zCm1d5oFC', true),
                       ('Caroline Herschel', 'candidate2@asg.com', '1001', '$2y$12$hiSBt4DuQMZZAdlaG7osu.EE21jvHklqHbRUovC6qQZPDyCXOQNG2', true),
                       ('Edmond Halley', 'candidate3@asg.com', '1001', '$2y$12$E9LNbS1h2VZ/lErbbWunVOpA67QJrPmPCqCBuGuLKuMY/r.QjEjea', true),
                       ('Enrico Fermi', 'candidate4@asg.com', '1001', '$2y$12$YGA1lrJ7MlhgKtNU0qy4UOtrvlYpswFlMjVrqDdaFQLTS54/MYhyy', true);

                INSERT INTO addresses(id, line_1, line_2, city, post_code)
                VALUES (1, 'Unit 14, 3 Edgar Buildings', 'George Street', 'Bath', 'BA1 2FJ'),
                       (2, 'Box 777, 91 Western Road', 'Brighton', 'East Sussex', 'BN1 2NW'),
                       (3, 'Room 67, 14 Tottenham Court Road', '', 'London', 'W1T 1JY');

                INSERT INTO contact_information (id, address_id, phone_number)
                VALUES (1, 1, '7597268597'),
                       (2, 2, '7911123456'),
                       (3, 3, '7911564856');

                INSERT INTO drones (make, model)
                VALUES ('DJI', 'Matrice'),
                       ('DJI', 'Mavic'),
                       ('DJI', 'Mavic 2'),
                       ('DJI', 'Mavic Air'),
                       ('DJI', 'Mavic Pro'),
                       ('DJI', 'Mavic Pro Platinum'),
                       ('DJI', 'Phantom 1'),
                       ('DJI', 'Phantom FC40'),
                       ('DJI', 'Phantom 2'),
                       ('DJI', 'Phantom 2 Vision'),
                       ('DJI', 'Phantom 2 Vision+'),
                       ('DJI', 'Phantom 3 Standard'),
                       ('DJI', 'Phantom 3 4k'),
                       ('DJI', 'Phantom 3 SE'),
                       ('DJI', 'Phantom 3 Advanced'),
                       ('DJI', 'Phantom 3 Professional'),
                       ('DJI', 'Phantom 4'),
                       ('DJI', 'Phantom 4 Advanced'),
                       ('DJI', 'Phantom 4 Pro'),
                       ('DJI', 'Phantom 4 Pro V2.0'),
                       ('DJI', 'Inspire 1'),
                       ('DJI', 'Inspire 1 Pro/Raw'),
                       ('DJI', 'Inspire 2'),
                       ('Parrot', 'Anafi'),
                       ('Parrot', 'Bebop 2'),
                       ('Parrot', 'Bebop 2 Power'),
                       ('Parrot', 'Mambo');

                INSERT INTO general_information(id, english_speaking_level, place_of_birth, date_of_birth, preferred_course_location, drone_id, paid)
                VALUES (1, 6, 'Somerset', '1990-10-21', 'Somerset', 5, true),
                       (2, 6, 'Aberdeenshire', '1990-12-31', 'Aberdeenshire', 10, true),
                       (3, 6, 'Cardiff', '1990-11-15', 'Cardiff', 1, true);

                INSERT INTO candidates(id, user_id, reference_number, contact_info_id, general_info_id, last_completed_stage, registration_date)
                VALUES (1, 4, 'ASG-002-19-02', 1, 1, 4, '2018-03-03'),
                       (2, 5, 'ASG-003-19-02', 2, 2, 7, '2019-03-19'),
                       (3, 6, 'ASG-004-19-02', 3, 3, 12, '2019-03-16');

            ");
        }
    }
}