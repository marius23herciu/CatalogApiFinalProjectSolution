using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class AddressExtension
    {
        public static AddressToGet ToDto(this Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new AddressToGet
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                Number = address.Number
            };
        }
    }
}
