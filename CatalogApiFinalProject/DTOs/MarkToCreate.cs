using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class MarkToCreate
    {
        [Required(ErrorMessage = "Value is required.")]
        [Range(1, 10)]
        public int Value { get; set; }
        [Required(ErrorMessage = "Subject's Id is required.")]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Student's Id is required.")]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
    }
}
