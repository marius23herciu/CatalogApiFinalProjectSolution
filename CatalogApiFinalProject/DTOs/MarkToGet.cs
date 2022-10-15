using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class MarkToGet
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
