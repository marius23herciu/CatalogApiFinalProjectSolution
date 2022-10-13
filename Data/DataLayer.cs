using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Data
{
    public class DataLayer
    {
        private CatalogueDbContext ctx;
        public DataLayer(CatalogueDbContext context)
        {
            this.ctx = context;
        }

        public Subject AddSubject(string name)
        {
            var newSubject = new Subject
            {
                Name = name,
            };

            ctx.Add(newSubject);
            foreach (var student in ctx.Students)
            {
                student.Subjects.Add(newSubject);
            }
            ctx.SaveChanges();


            /////////verifica de null peste toooooottttttt
            if (newSubject != null)
            {
                return newSubject;
            }
            return null;
            ///////////////////////////////////////////////
        }
        public Mark AddMarkToStudent(int studentId, int subjectId, int value)
        {
            var student = ctx.Students.Where(s => s.Id == studentId).FirstOrDefault();

            var newMark = new Mark
            {
                Value = value,
                SubjectId = subjectId,
                DateTime = DateTime.Now,
            };
            student.Marks.Add(newMark);

            ctx.SaveChanges();

            return newMark;
        }
        public List<int> GetAllMarks(int id)
        {
            var student = ctx.Students.Include(m => m.Marks).Where(s => s.Id == id).FirstOrDefault();
            var marks = student.Marks.Select(v => v.Value).ToList();

            return marks;
        }
        public List<int> GetAllMarksForSpecificSubject(int studentId, int subjectId)
        {
            var student = ctx.Students.Include(m => m.Marks).Where(s => s.Id == studentId).FirstOrDefault();

            var subject = ctx.Subjects.Where(s => s.Id == subjectId).FirstOrDefault();

            var marks = student.Marks.Where(s => s.SubjectId == subjectId).Select(v => v.Value).ToList();

            return marks;
        }

        //public List<double> GetAllAveragesForOneStudent(int studentId)
        //{
        //    var student = ctx.Students.Include(s => s.Subjects).Include(m => m.Marks).Where(s => s.Id == studentId).FirstOrDefault();
        //    var marks = student.Marks.Select(v => v.Value).ToList();




        //    List<double> allAverages = new List<double>();

        //    var subjects = student.Subjects.ToList();

        //    double average = 0;
        //    double sum = 0;
        //    int counter = 0;
        //    foreach (var subject in subjects)
        //    {
        //        foreach (var mark in student.Marks)
        //        {
        //            if (mark.SubjectId == subject.Id)
        //            {
        //                sum += mark.Value;
        //                counter++;
        //            }
        //        }
        //        average = sum / counter;
        //        allAverages.Add(average);

        //        average = 0;
        //        sum = 0;
        //        counter = 0;
        //    }

        //    return allAverages;
        //}
        //public List<double> GetAllAveragesInOrder(bool ascendingOrder)
        //{
        //    var students = ctx.Students.Include(s => s.Subjects).Include(m => m.Marks).ToList();
        //    var subjects = ctx.Subjects.ToList();

        //    double average = 0;
        //    double sum = 0;
        //    int counter = 0;

        //    double sumOfAverages = 0;
        //    double finalAverage = 0;
        //    int counterOfAverages = 0;

        //    List<double> listOfAverages = new List<double>();

        //    foreach (var student in students)
        //    {
        //        foreach (var subject in subjects)
        //        {
        //            foreach (var mark in student.Marks)
        //            {
        //                if (subject.Id == mark.SubjectId)
        //                {
        //                    sum += mark.Value;
        //                    counter++;
        //                }
        //            }
        //            if (sum > 0)
        //            {
        //                average = sum / counter;
        //                sumOfAverages += average;
        //                counterOfAverages++;
        //            }
        //            sum = 0;
        //            counter = 0;
        //            average = 0;
        //        }
        //        if (sumOfAverages > 0)
        //        {
        //            finalAverage = sumOfAverages / counterOfAverages;
        //        }
        //        listOfAverages.Add(finalAverage);
        //        sumOfAverages = 0;
        //        finalAverage = 0;
        //        counterOfAverages = 0;
        //    }

        //    if (ascendingOrder == true)
        //    {
        //        listOfAverages.Sort();
        //    }
        //    else
        //    {
        //        List<double> temp = new List<double>();
        //        foreach (double d in listOfAverages.OrderByDescending(d => d))
        //        {
        //            temp.Add(d);
        //        }
        //        listOfAverages = temp;
        //    }

        //    return listOfAverages;
        //}
        public List<Student> GetAllStudents()
        {
            var students = ctx.Students.Include(s => s.Subjects).ToList();
            return students;
        }
        public Student GetStudentById(int id)
        {
            var student = ctx.Students.Where(s => s.Id == id).FirstOrDefault();
            return student;
        }
        public Student CreateStudent(string firstName, string lastName, int age)
        {
            var subjects = ctx.Subjects.ToList();

            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Subjects = subjects,
            };

            ctx.Add(newStudent);
            ctx.SaveChanges();

            return newStudent;
        }
        public void DeleteStudent(int studentId)
        {
            var studentToRemove = ctx.Students.Include(a => a.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            studentToRemove.Adresse = null;
            ctx.Students.Remove(studentToRemove);
            ctx.SaveChanges();
        }
        public void ChangeStudentData(int studentId, string firstName, string lastName, int age)
        {
            var student = ctx.Students.Where(s => s.Id == studentId).FirstOrDefault();

            if (firstName != string.Empty)
            {
                student.FirstName = firstName;
            }
            if (lastName != string.Empty)
            {
                student.LastName = lastName;
            }
            if (age != null)
            {
                student.Age = age;
            }

            ctx.SaveChanges();
        }

        public void ChangeStudentAddress(int studentId, string city, string street, int number)
        {
            var student = ctx.Students.Include(s => s.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            if (student.Adresse == null)
            {
                student.Adresse = new Address
                {
                    City = city,
                    Street = street,
                    Number = number,
                };
            }
            else
            {
                var adresseToChange = student.Adresse;
                adresseToChange.City = city;
                adresseToChange.Street = street;
                adresseToChange.Number = number;
            }

            ctx.SaveChanges();
        }
        public void DeleteStudentAndAddress(int studentId, bool deleteAddress)
        {
            var studentToRemove = ctx.Students.Include(a => a.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            if (studentToRemove != null)
            {
                if (deleteAddress == true)
                {
                    var adresseToRemove = ctx.Adresses.Where(s => s.StudentId == studentId).FirstOrDefault();
                    ctx.Adresses.Remove(adresseToRemove);
                    ctx.Students.Remove(studentToRemove);
                }
                else
                {
                    ctx.Students.Remove(studentToRemove);
                }
            }

            ctx.SaveChanges();
        }
        public void DeleteSubject(int subjectId, bool keepMarks)
        {
            var subjectToDelete = ctx.Subjects.Include(m => m.Marks).Include(t => t.Teacher).Where(s => s.Id == subjectId).FirstOrDefault();

            if (keepMarks != true)
            {
                var marks = ctx.Marks.Where(m => m.SubjectId == subjectId).ToList();
                ctx.Marks.RemoveRange(marks);
            }

            ctx.Subjects.Remove(subjectToDelete);
            ctx.SaveChanges();
        }
        public Teacher CreateTeacher(string firstName, string lastName, Rank rank)
        {
            var newTeacher = new Teacher
            {
                FirstName = firstName,
                LastName = lastName,
                Rank = rank,
            };

            ctx.Add(newTeacher);
            ctx.SaveChanges();

            return newTeacher;
        }
        public void DeleteTeacher(int teacherId)
        {
            var teacherToDelete = ctx.Teachers.Include(a => a.Address).Where(s => s.Id == teacherId).FirstOrDefault();

            var addressToRemove = ctx.Adresses.Where(t => t.TeacherId == teacherId).FirstOrDefault();

            if (addressToRemove != null && addressToRemove.StudentId == null)
            {
                ctx.Adresses.Remove(addressToRemove);
            }

            teacherToDelete.Subject = null;
            ctx.Teachers.Remove(teacherToDelete);
            ctx.SaveChanges();
        }
        public void ChangeTeachersAddress(int teacherId, string city, string street, int number)
        {
            var teacher = ctx.Teachers.Include(s => s.Address).Where(t => t.Id == teacherId).FirstOrDefault();

            if (teacher.Address == null)
            {
                teacher.Address = new Address
                {
                    City = city,
                    Street = street,
                    Number = number,
                };
            }
            else
            {
                var addressToChange = teacher.Address;
                addressToChange.City = city;
                addressToChange.Street = street;
                addressToChange.Number = number;
            }

            ctx.SaveChanges();
        }
        public void GivesCourseToTeacher(int teacherId, int subjectId)
        {
            var teacher = ctx.Teachers.Where(t => t.Id == teacherId).Include(s => s.Subject).FirstOrDefault();

            var subjects = ctx.Subjects.Select(s => s.Id).ToList();

            var subject = ctx.Subjects.Where(s => s.Id == subjectId).FirstOrDefault();
            teacher.Subject = subject;
            ctx.SaveChanges();
        }
        public void PromoteTeacher(int teacherId)
        {
            var teacher = ctx.Teachers.Where(t => t.Id == teacherId).FirstOrDefault();

            teacher.Rank++;
            ctx.SaveChanges();
        }
        public List<int> GetAllMarksGivenByTeacher(int teacherId)
        {
            var teacher = ctx.Teachers.Include(m => m.Subject).Where(s => s.Id == teacherId).FirstOrDefault();

            var marks = ctx.Marks.Where(m => m.SubjectId == teacher.SubjectId).Select(v => v.Value).ToList();

            return marks;
        }
    }
}
