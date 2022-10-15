using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class AddressToGet
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
