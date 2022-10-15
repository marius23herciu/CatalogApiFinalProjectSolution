using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class StudentToGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
