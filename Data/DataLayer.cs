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

            return newSubject;
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
       
    }
}
