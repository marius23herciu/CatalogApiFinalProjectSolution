using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Adresse { get; set; }
        public List<Mark> Marks { get; set; } = new List<Mark>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
