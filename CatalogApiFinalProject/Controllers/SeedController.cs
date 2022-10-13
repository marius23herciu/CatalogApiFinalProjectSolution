using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly CatalogueDbContext context;

        public SeedController(CatalogueDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Seeds/Resets Database.
        /// </summary>
        [HttpPost]
        public void Seed()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            if (context.Students.Count() != 0)
            {
                return;
            }
            var math = new Subject()
            {
                Name = "Mathematics",
                Teacher = new Teacher()
                {
                    FirstName = "Ioan",
                    LastName = "Popescu",
                    Rank = Rank.Professor,
                    Address = new Address()
                    {
                        City = "Bucuresti",
                        Street = "Universitatii",
                        Number = 54
                    }
                }
            };
            var english = new Subject()
            {
                Name = "English",
                Teacher = new Teacher()
                {
                    FirstName = "Ana",
                    LastName = "Vranceanu",
                    Rank = Rank.Instructor,
                    Address = new Address()
                    {
                        City = "Bucuresti",
                        Street = "Republicii",
                        Number = 47
                    }
                }
            };
            var geography = new Subject()
            {
                Name = "Geography",
                Teacher = new Teacher()
                {
                    FirstName = "Gheorghe",
                    LastName = "Anton",
                    Rank = Rank.Associate_Professor,
                    Address = new Address()
                    {
                        City = "Bucuresti",
                        Street = "Libertatii",
                        Number = 14
                    }
                }
            };
            var history = new Subject()
            {
                Name = "History",
                Teacher = new Teacher()
                {
                    FirstName = "Andreea",
                    LastName = "Postelnicu",
                    Rank = Rank.Assistant_Professor,
                    Address = new Address()
                    {
                        City = "Bucuresti",
                        Street = "Tineretului",
                        Number = 20
                    }
                },
            };

            var subjects = new List<Subject>();
            subjects.Add(math);
            subjects.Add(history);
            subjects.Add(english);
            subjects.Add(geography);


            var studentsMarks1 = new List<Mark>();


            studentsMarks1.Add(new Mark
            {
                Value = 7,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 10,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 7,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 9,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks1.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });

            context.Students.Add(new Student
            {
                FirstName = "Ionut",
                LastName = "Antonescu",
                Age = 20,
                Adresse = new Address
                {
                    City = "Bucuresti",
                    Street = "Obor",
                    Number = 12
                },
                Marks = studentsMarks1,
                Subjects = subjects,
            });

            var studentsMarks2 = new List<Mark>();


            studentsMarks2.Add(new Mark
            {
                Value = 4,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 5,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 8,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 6,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks2.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });

            context.Students.Add(new Student
            {
                FirstName = "Ioana",
                LastName = "Popescu",
                Age = 20,
                Adresse = new Address
                {
                    City = "Bucuresti",
                    Street = "Mare",
                    Number = 85
                },
                Marks = studentsMarks2,
                Subjects = subjects,
            });

            var studentsMarks3 = new List<Mark>();


            studentsMarks3.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 2,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 5,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks3.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Dan",
                LastName = "Bitman",
                Age = 45,
                Adresse = new Address
                {
                    City = "Bucuresti",
                    Street = "Revolutiei",
                    Number = 44
                },
                Marks = studentsMarks3,
                Subjects = subjects,
            });

            var studentsMarks4 = new List<Mark>();


            studentsMarks4.Add(new Mark
            {
                Value = 5,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 8,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 3,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks4.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Crina",
                LastName = "Manea",
                Age = 30,
                Adresse = new Address
                {
                    City = "Iasi",
                    Street = "Pacurari",
                    Number = 56
                },
                Marks = studentsMarks4,
                Subjects = subjects,
            });

            var studentsMarks5 = new List<Mark>();


            studentsMarks5.Add(new Mark
            {
                Value = 3,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 7,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 10,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 9,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks5.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "George",
                LastName = "Trifan",
                Age = 22,
                Adresse = new Address
                {
                    City = "Timisoara",
                    Street = "Bega",
                    Number = 25
                },
                Marks = studentsMarks5,
                Subjects = subjects,
            });


            var studentsMarks6 = new List<Mark>();


            studentsMarks6.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 5,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 8,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks6.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Paul",
                LastName = "Dobre",
                Age = 18,
                Adresse = new Address
                {
                    City = "Arad",
                    Street = "Viilor",
                    Number = 64
                },
                Marks = studentsMarks6,
                Subjects = subjects,
            });

            var studentsMarks7 = new List<Mark>();


            studentsMarks7.Add(new Mark
            {
                Value = 9,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks7.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });



            context.Students.Add(new Student
            {
                FirstName = "Diana",
                LastName = "Velescu",
                Age = 25,
                Adresse = new Address
                {
                    City = "Ramnicu Valcea",
                    Street = "Oltulu",
                    Number = 17
                },
                Marks = studentsMarks7,
                Subjects = subjects,
            });

            var studentsMarks8 = new List<Mark>();


            studentsMarks8.Add(new Mark
            {
                Value = 4,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 5,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 5,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 3,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 5,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks8.Add(new Mark
            {
                Value = 4,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "John",
                LastName = "Carpenter",
                Age = 21,
                Adresse = new Address
                {
                    City = "Newcastle",
                    Street = "Harbour",
                    Number = 78
                },
                Marks = studentsMarks8,
                Subjects = subjects,
            });


            var studentsMarks9 = new List<Mark>();


            studentsMarks9.Add(new Mark
            {
                Value = 9,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 7,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 6,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 5,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks9.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });



            context.Students.Add(new Student
            {
                FirstName = "Ionut",
                LastName = "Antonescu",
                Age = 22,
                Adresse = new Address
                {
                    City = "Paris",
                    Street = "Lumiere",
                    Number = 7
                },
                Marks = studentsMarks9,
                Subjects = subjects,
            });


            var studentsMarks10 = new List<Mark>();


            studentsMarks10.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 2,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks10.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });




            context.Students.Add(new Student
            {
                FirstName = "Lucian",
                LastName = "Coman",
                Age = 31,
                Adresse = new Address
                {
                    City = "Constanta",
                    Street = "Portului",
                    Number = 12
                },
                Marks = studentsMarks10,
                Subjects = subjects,
            });

            var studentsMarks11 = new List<Mark>();


            studentsMarks11.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 7,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 4,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 9,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 5,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 9,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks11.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Andreea",
                LastName = "Caliman",
                Age = 35,
                Adresse = new Address
                {
                    City = "Galati",
                    Street = "Dunarii",
                    Number = 21
                },
                Marks = studentsMarks11,
                Subjects = subjects,
            });

            var studentsMarks12 = new List<Mark>();


            studentsMarks12.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 2,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks12.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "George",
                LastName = "Antonescu",
                Age = 20,
                Adresse = new Address
                {
                    City = "Cluj",
                    Street = "Transilvaniei",
                    Number = 55
                },
                Marks = studentsMarks12,
                Subjects = subjects,
            });

            var studentsMarks13 = new List<Mark>();


            studentsMarks13.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 5,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 9,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 7,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 5,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks13.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Andreea",
                LastName = "Coman",
                Age = 18,
                Adresse = new Address
                {
                    City = "Craiova",
                    Street = "Universitatii",
                    Number = 14
                },
                Marks = studentsMarks13,
                Subjects = subjects,
            });

            var studentsMarks14 = new List<Mark>();


            studentsMarks14.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 6,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 6,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 6,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks14.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Razvan",
                LastName = "Florea",
                Age = 25,
                Adresse = new Address
                {
                    City = "Suceava",
                    Street = "Bucovinei",
                    Number = 78
                },
                Marks = studentsMarks14,
                Subjects = subjects,
            });

            var studentsMarks15 = new List<Mark>();


            studentsMarks15.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 9,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 8,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 4,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 3,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks15.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Gheorghe",
                LastName = "Gherghel",
                Age = 42,
                Adresse = new Address
                {
                    City = "Madrid",
                    Street = "Huevos",
                    Number = 88
                },
                Marks = studentsMarks15,
                Subjects = subjects,
            });

            var studentsMarks16 = new List<Mark>();


            studentsMarks16.Add(new Mark
            {
                Value = 6,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 5,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 2,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 6,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 10,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks16.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Izabela",
                LastName = "Popa",
                Age = 21,
                Adresse = new Address
                {
                    City = "Roma",
                    Street = "Pizzeria",
                    Number = 22
                },
                Marks = studentsMarks16,
                Subjects = subjects,
            });

            var studentsMarks17 = new List<Mark>();


            studentsMarks17.Add(new Mark
            {
                Value = 7,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 8,
                Subject = math,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 10,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 8,
                Subject = english,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 9,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 10,
                Subject = geography,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 8,
                Subject = history,
                DateTime = DateTime.Now,
            });
            studentsMarks17.Add(new Mark
            {
                Value = 7,
                Subject = history,
                DateTime = DateTime.Now,
            });


            context.Students.Add(new Student
            {
                FirstName = "Sebi",
                LastName = "Popa",
                Age = 21,
                Adresse = new Address
                {
                    City = "Brasov",
                    Street = "Cerbului",
                    Number = 24
                },
                Marks = studentsMarks17,
                Subjects = subjects,
            });


            context.SaveChanges();
        }
    }
}
