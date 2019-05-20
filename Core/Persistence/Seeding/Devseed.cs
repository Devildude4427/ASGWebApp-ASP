using System.Threading.Tasks;
using Autofac;
using Core.Persistence.Configuration;
using Dapper;

namespace Core.Persistence.Seeding
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
                       ('Enrico Fermi', 'candidate4@asg.com', '1001', '$2y$12$YGA1lrJ7MlhgKtNU0qy4UOtrvlYpswFlMjVrqDdaFQLTS54/MYhyy', true),
                       ('Suzie Sawday', 'ssawday0@zimbio.com', 1001, 'CqUAAF6E84', true),
                       ('Chere Marryatt', 'cmarryatt1@google.de', 1001, 'mKEGBS0',true),
                       ('Orelie Bache', 'obache2@xrea.com', 1001, 'DLrFylYG', true),
                       ('Manny Cousins', 'mcousins3@pcworld.com', 1001, 'fTmhmNKR', true),
                       ('Caye Ardron', 'cardron4@wunderground.com', 1001, 'n6R9xXu', true),
                       ('Latrina Yeowell', 'lyeowell5@skype.com', 1001, 'lhR2U0mRX', true),
                       ('Elle Castelijn', 'ecastelijn6@live.com', 1001, '9HxXhQo9', true),
                       ('Roselia Worral', 'rworral7@engadget.com', 1001, '6zEduR5', true),
                       ('Edyth Amberson', 'eamberson8@blinklist.com', 1001, 'eeuN3MVT9mYT', true),
                       ('Dulsea Deane', 'ddeane9@domainmarket.com', 1001, 'V4IAzwl', true),
                       ('Barthel Camolli', 'bcamollia@marriott.com', 1001, '8tnmwzUz', true),
                       ('Yoshi Presman', 'ypresmanb@sphinn.com', 1001, 'ae6Ub6', true),
                       ('Kathy Liccardo', 'kliccardoc@yolasite.com', 1001, '0xidXZV', true),
                       ('Osmund Fetherstone', 'ofetherstoned@ucla.edu', 1001, 'LGdNfIhKI', true),
                       ('Isak Ellerbeck', 'iellerbecke@ebay.co.uk', 1001, 'SgSTzue249Y', true),
                       ('Austen Patters', 'apattersf@oakley.com', 1001, 'YF6HHMEE8X', true),
                       ('Avril Crann', 'acranng@example.com', 1001, 'VG0zKCu', true),
                       ('Maxim Craddock', 'mcraddockh@cnn.com', 1001, 'HAIDPT1G', true),
                       ('Mada Drissell', 'mdrisselli@pen.io', 1001, 't4DzXLsB1aH', true),
                       ('Rakel Hearnden', 'rhearndenj@nyu.edu', 1001, 'gP7ZszB', true),
                       ('Donovan Sibthorpe', 'dsibthorpek@unicef.org', 1001, '0rWe9FN40', true),
                       ('Constantine Aimer', 'caimerl@cdbaby.com', 1001, 'k4Eej4tXU3', true),
                       ('Domenico Simeoni', 'dsimeonim@amazon.com', 1001, '2EIX0Ca', true),
                       ('Erminia Bonafant', 'ebonafantn@forbes.com', 1001, 'AKDKL0', true),
                       ('Maryanne Say', 'msayo@so-net.ne.jp', 1001, 'TGnXMwZhQt', true),
                       ('Krispin Sanbrook', 'ksanbrookp@reddit.com', 1001, 'o1NHi8Ir46X', true),
                       ('Prisca Bukowski', 'pbukowskiq@tripod.com', 1001, 'dlbNtwSEqAw', true),
                       ('Shaw Phelps', 'sphelpsr@comsenz.com', 1001, 'Y0sHPw0yD', true),
                       ('Elke Durant', 'edurants@smugmug.com', 1001, 'APrIqqrRnE', true),
                       ('Annamaria Gomersal', 'agomersalt@rediff.com', 1001, 'L9U96Jj7D', true),
                       ('Kane Arnoult', 'karnoultu@cocolog-nifty.com', 1001, 'H5iGRpJ9oBxZ', true),
                       ('Harlin Stollery', 'hstolleryv@nhs.uk', 1001, '0Umvx5hQdkL', true),
                       ('Isidro Rusling', 'iruslingw@sciencedaily.com', 1001, 'h878fgoMY', true),
                       ('Alane Adolphine', 'aadolphinex@shop-pro.jp', 1001, 'uu47pdRl3C', true),
                       ('Sammy Thompson', 'sthompsony@i2i.jp', 1001, 'PCIDA1', true),
                       ('Avivah Butter', 'abutterz@is.gd', 1001, 'yibzfBzsW', true),
                       ('Alejandra Raff', 'araff10@boston.com', 1001, '1a1RSqZ', true),
                       ('Darnall Conant', 'dconant11@symantec.com', 1001, 'U64sDeL2', true),
                       ('Griswold Ackrill', 'gackrill12@prweb.com', 1001, 'jbi3bPtRl4n', true),
                       ('Julissa Wyant', 'jwyant13@sfgate.com', 1001, 'r1vhak', true),
                       ('Bambi Howse', 'bhowse14@trellian.com', 1001, 'jEuJdbR', true),
                       ('Mitchael Rougier', 'mrougier15@columbia.edu', 1001, 'bW2arCQEO2', true),
                       ('Malynda Lebrun', 'mlebrun16@wufoo.com', 1001, 'QHdyXzlEuw', true),
                       ('Kerr Grimston', 'kgrimston17@github.com', 1001, 'jPDpo2y', true),
                       ('Manda Sparshutt', 'msparshutt18@zdnet.com', 1001, 'RKJrxzDz', true),
                       ('Travus De Goey', 'tde19@themeforest.net', 1001, '5PMvGWg', true),
                       ('Fara Garioch', 'fgarioch1a@hibu.com', 1001, 'hmVLz17esGn9', true),
                       ('Dorice Booth-Jarvis', 'dboothjarvis1b@nasa.gov', 1001, 'zdhVFZxl4nZR', true),
                       ('Jessee Goggen', 'jgoggen1c@canalblog.com', 1001, 'xCqdzx', true),
                       ('Carlin Sabberton', 'csabberton1d@sogou.com', 1001, 'g6IElUn', true),
                       ('Reina Hartfleet', 'rhartfleet1e@stumbleupon.com', 1001, 'zDMtEHlz', true),
                       ('Dev Heminsley', 'dheminsley1f@ucoz.com', 1001, 'LBUJGbzeLdo', true),
                       ('Mommy Lawless', 'mlawless1g@usa.gov', 1001, '4GtehUcN', true),
                       ('Nanny Handrock', 'nhandrock1h@nature.com', 1001, 'Wo1tHOrkg', true),
                       ('Milt de Amaya', 'mde1i@businesswire.com', 1001, '18B8dvYRGhP', true),
                       ('Petronella Baylay', 'pbaylay1j@dot.gov', 1001, 'Yti7NTnQF', true),
                       ('Sosanna Wasbey', 'swasbey1k@nasa.gov', 1001, 'QGGeS4rKT2f', true),
                       ('Talbert Percy', 'tpercy1l@engadget.com', 1001, 'gBlJvydBZ', true),
                       ('Merry Erb', 'merb1m@go.com', 1001, '2Fg3wogY9M3', true),
                       ('Ara Schlag', 'aschlag1n@feedburner.com', 1001, 'NAwL7C9', true),
                       ('Jacquie Steers', 'jsteers1o@moonfruit.com', 1001, 'iCz53AP3aR', true),
                       ('Forbes Morteo', 'fmorteo1p@si.edu', 1001, 'tGmSKZv91zM', true),
                       ('Timmy Drever', 'tdrever1q@usnews.com', 1001, 'km6V9FDAtB4', true),
                       ('Rosabella McCoish', 'rmccoish1r@xinhuanet.com', 1001, 'HCUZTi', true),
                       ('Cad Offa', 'coffa1s@blogs.com', 1001, 'kPK5Eda', true),
                       ('Tybalt Kenchington', 'tkenchington1t@cmu.edu', 1001, 'WfZTj4urrIu', true),
                       ('Lina Edmundson', 'ledmundson1u@chronoengine.com', 1001, 'XjYhAj2', true),
                       ('Malory Barge', 'mbarge1v@ezinearticles.com', 1001, 'S2dRT5Mwai', true),
                       ('Arden Ovendon', 'aovendon1w@barnesandnoble.com', 1001, 'xtX0KNX', true),
                       ('Hollis Breffitt', 'hbreffitt1x@sbwire.com', 1001, 't0MPxZBcn', true),
                       ('Christen Wadlow', 'cwadlow1y@sogou.com', 1001, 'kdsrXBoQlH', true),
                       ('Bobbie Klicher', 'bklicher1z@comsenz.com', 1001, 'cszpKR', true),
                       ('Jordain Glinde', 'jglinde20@multiply.com', 1001, 'gNrUh3AXrg', true),
                       ('Chelsy Pearlman', 'cpearlman21@pagesperso-orange.fr', 1001, 'j8g4sw', true),
                       ('Eada Goodlatt', 'egoodlatt22@examiner.com', 1001, 'ec3xrOlVBRA', true),
                       ('Katleen Brown', 'kbrown23@icio.us', 1001, '5gOPZv', true),
                       ('Odelia Jurkowski', 'ojurkowski24@yahoo.co.jp', 1001, 'TkbAJqjQ', true),
                       ('Deni Raincin', 'draincin25@weibo.com', 1001, 'y4z3QXj', true),
                       ('Pattin McMennum', 'pmcmennum26@irs.gov', 1001, 'tCEl93', true),
                       ('Gretta Elvin', 'gelvin27@photobucket.com', 1001, '9dQArV4', true),
                       ('Benedikt Chalker', 'bchalker28@virginia.edu', 1001, 'o0PWmP', true),
                       ('Magdalene Innocenti', 'minnocenti29@scientificamerican.com', 1001, 'db38GAW', true),
                       ('Toma Jouhning', 'tjouhning2a@mail.ru', 1001, 'fAdcsWOEz', true),
                       ('Debora Koles', 'dkoles2b@ucoz.ru', 1001, 'cZE3sBOBdsI4', true),
                       ('Ring Remmer', 'rremmer2c@wordpress.org', 1001, 'VjUrBx', true),
                       ('Mickey Concklin', 'mconcklin2d@wikipedia.org', 1001, 'IjonX2Dyzln', true),
                       ('Renata Sturley', 'rsturley2e@google.co.jp', 1001, 'aR3mjBkED', true),
                       ('Leicester Hoyle', 'lhoyle2f@state.gov', 1001, 'sXkIdcsjFBj', true),
                       ('Dulcia Tomich', 'dtomich2g@privacy.gov.au', 1001, 'pRQXXJIo4rbk',true),
                       ('Clark Ranking', 'cranking2h@chicagotribune.com', 1001, 'PFEMsryFdF9', true),
                       ('Had Scafe', 'hscafe2i@scientificamerican.com', 1001, 'uBP0eSwpAz5', true),
                       ('Bearnard Dallender', 'bdallender2j@opera.com', 1001, 'FfsSFzoNug', true),
                       ('Roland Rodenhurst', 'rrodenhurst2k@wordpress.com', 1001, 'nnttziZiseP', true),
                       ('Gretal Bartczak', 'gbartczak2l@yahoo.co.jp', 1001, 'mEjeTq', true),
                       ('Ophelie Duferie', 'oduferie2m@weather.com', 1001, 'OhHkXIfxsFc', true),
                       ('Annmaria Tidbold', 'atidbold2n@homestead.com', 1001, 'lxdaAqNK3f5', true);


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
                VALUES (1, 4, 'ASG-19-02-002', 1, 1, 4, '2018-03-03'),
                       (2, 5, 'ASG-19-02-003', 2, 2, 7, '2019-03-19'),
                       (3, 6, 'ASG-19-02-004', 3, 3, 12, '2019-03-16');

            ");
        }
    }
}