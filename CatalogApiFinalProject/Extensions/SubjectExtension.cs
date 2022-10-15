using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class SubjectExtension
    {
        public static SubjectToGet ToDto(this Subject subject)
        {
            if (subject == null)
            {
                return null;
            }
            return
                new SubjectToGet
                {
                    Id = subject.Id,
                    Name = subject.Name,
                };
        }
    }
}
