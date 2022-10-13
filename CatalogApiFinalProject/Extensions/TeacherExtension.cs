using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class TeacherExtension
    {
        public static TeacherToGet ToDto(this Teacher teacher)
        {
            return
                new TeacherToGet
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                };
        }
    }
}
