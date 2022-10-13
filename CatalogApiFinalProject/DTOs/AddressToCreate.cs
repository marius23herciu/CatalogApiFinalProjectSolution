using System.ComponentModel.DataAnnotations;

namespace CatalogApiFinalProject.DTOs
{
    public class AddressToCreate
    {
        [Required(ErrorMessage = "City has to be in Address.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street has to be in Address.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number has to be in Address.")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
    }
}
