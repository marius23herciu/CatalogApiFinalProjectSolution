using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class StudentExtension
    {
        public static StudentToGet ToDto(this Student student)
        {
            return
                new StudentToGet
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                };
        }
    }
}
