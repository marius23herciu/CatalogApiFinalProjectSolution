using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class StudentWithAddressExtension
    {
        public static StudentWithAddressToGet ToDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }
            
            return
                new StudentWithAddressToGet
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Address = student.Address.ToDto(),
                };
        }
    }
}
