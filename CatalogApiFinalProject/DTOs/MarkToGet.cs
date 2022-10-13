using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class MarkToGet
    {
        [Required(ErrorMessage = "Mark Id is required.")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Value is required.")]
        [Range(1, 10)]
        public int Value { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
