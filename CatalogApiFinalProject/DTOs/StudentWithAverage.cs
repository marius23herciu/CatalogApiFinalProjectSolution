using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class StudentWithAverage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Average { get; set; }
    }
}
