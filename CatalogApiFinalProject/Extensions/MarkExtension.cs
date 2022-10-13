using CatalogApiFinalProject.DTOs;
using Data.Models;

namespace CatalogApiFinalProject.Extensions
{
    public static class MarkExtension
    {
        public static MarkToGet ToDto(this Mark mark)
        {
            return
                new MarkToGet
                {
                    Id = mark.Id,
                    DateTime = mark.DateTime,
                    Value = mark.Value,
                };
        }
    }
}
