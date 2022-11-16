using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data
{
    public class DataLayer
    {
        private CatalogueDbContext ctx;
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public DataLayer(CatalogueDbContext context)
        {
            this.ctx = context;
        }


        public void FullBackup()
        {
            BackupStudents();
            BackupTeacherss();
            BackupAddresses();
            BackupSubjects();
            BackupMarks();
        }
        public void FullRestore()
        {
            RestoreStudents();
            RestoreAddresses();
            RestoreTeachers();
            RestoreSubjects();
            RestoreMarks();
        }


        public void BackupStudents()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\studentsBackupDb.txt";
            File.Delete(path);
            File.AppendAllText(path, JsonSerializer.Serialize(ctx.Students, options));
        }
        public void RestoreStudents()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\studentsBackupDb.txt";
            var serializedStudents = File.ReadAllText(path);
            var students = JsonSerializer.Deserialize<List<Student>>(serializedStudents, options);
            if (students != null)
            {
                foreach (var student in ctx.Students)
                {
                    ctx.Students.Remove(student);
                }

                foreach (var student in students)
                {
                    ctx.Students.Add(student);
                }
                ctx.SaveChanges();
            }
        }

        public void BackupSubjects()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\subjectsBackupDb.txt";
            File.Delete(path);
            File.AppendAllText(path, JsonSerializer.Serialize(ctx.Subjects, options));
        }
        public void RestoreSubjects()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\subjectsBackupDb.txt";
            var serializedSubjects = File.ReadAllText(path);
            var subjects = JsonSerializer.Deserialize<List<Subject>>(serializedSubjects, options);
            if (subjects != null)
            {
                foreach (var subject in ctx.Subjects)
                {
                    ctx.Subjects.Remove(subject);
                }

                foreach (var subject in subjects)
                {
                    ctx.Subjects.Add(subject);
                }
                ctx.SaveChanges();
            }
        }


        public void BackupAddresses()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\addressesBackupDb.txt";
            File.Delete(path);
            File.AppendAllText(path, JsonSerializer.Serialize(ctx.Adresses, options));
        }
        public void RestoreAddresses()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\addressesBackupDb.txt";
            var serializedAddresses = File.ReadAllText(path);
            var addresses = JsonSerializer.Deserialize<List<Address>>(serializedAddresses, options);
            if (addresses != null)
            {
                foreach (var address in ctx.Adresses)
                {
                    ctx.Adresses.Remove(address);
                }

                foreach (var address in addresses)
                {
                    ctx.Adresses.Add(address);
                }
                ctx.SaveChanges();
            }
        }


        public void BackupMarks()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\marksBackupDb.txt";
            File.Delete(path);
            File.AppendAllText(path, JsonSerializer.Serialize(ctx.Marks, options));
        }
        public void RestoreMarks()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\marksBackupDb.txt";
            var serializedMarks = File.ReadAllText(path);
            var marks = JsonSerializer.Deserialize<List<Mark>>(serializedMarks, options);
            if (marks != null)
            {
                foreach (var mark in ctx.Marks)
                {
                    ctx.Marks.Remove(mark);
                }

                foreach (var mark in marks)
                {
                    ctx.Marks.Add(mark);
                }
                ctx.SaveChanges();
            }
        }


        public void BackupTeacherss()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\teachersBackupDb.txt";
            File.Delete(path);
            File.AppendAllText(path, JsonSerializer.Serialize(ctx.Teachers, options));
        }
        public void RestoreTeachers()
        {
            var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\ProiectFinal\backupDb\teachersBackupDb.txt";
            var serializedTeachers = File.ReadAllText(path);
            var teachers = JsonSerializer.Deserialize<List<Teacher>>(serializedTeachers, options);
            if (teachers != null)
            {
                foreach (var teacher in ctx.Teachers)
                {
                    ctx.Teachers.Remove(teacher);
                }

                foreach (var teacher in teachers)
                {
                    ctx.Teachers.Add(teacher);
                }
                ctx.SaveChanges();
            }
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
