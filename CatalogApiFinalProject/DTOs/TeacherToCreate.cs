using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class TeacherToCreate
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Rank is required.")]
        [Range(0, 3)]
        public Rank Rank { get; set; }
    }
}
