using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class SubjectToCreate
    {
        [Required(ErrorMessage = "Subject's name is required.")]
        public string Name { get; set; }
    }
}
