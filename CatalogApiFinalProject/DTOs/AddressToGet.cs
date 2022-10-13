using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class AddressToGet
    {
        [Required(ErrorMessage = "Address Id is required.")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "City has to be in Address.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street has to be in Address.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number has to be in Address.")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
    }
}
