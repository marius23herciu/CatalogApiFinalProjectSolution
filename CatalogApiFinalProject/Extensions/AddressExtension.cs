using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class AddressExtension
    {
        public static AddressToGet ToDto(this Address adresse)
        {
            return
                new AddressToGet
                {
                    City = adresse.City,
                    Street = adresse.Street,
                    Number = adresse.Number,
                };
        }
    }
}
