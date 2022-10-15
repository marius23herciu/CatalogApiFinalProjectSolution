using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class TeacherToGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
