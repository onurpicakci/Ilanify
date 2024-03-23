using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Extensions;

public static class RealEstateQueryExtensions
{
    public static IQueryable<RealEstate> ApplyFilter(this IQueryable<RealEstate> query, RealEstateFilter filter)
    {
        if (filter.CategoryId.HasValue)
        {
            query = query.Where(re => re.CategoryId == filter.CategoryId);
        }

        if (!string.IsNullOrEmpty(filter.City))
        {
            query = query.Where(re => re.Location.City == filter.City);
        }

        if (filter.MinRooms.HasValue)
        {
            query = query.Where(re => re.AttributeValues
                .Any(av => av.CategoryAttribute.Name == "Oda Sayısı" && int.Parse(av.Value) >= filter.MinRooms.Value));
        }

        if (filter.MaxRooms.HasValue)
        {
            query = query.Where(re => re.AttributeValues
                .Any(av => av.CategoryAttribute.Name == "Oda Sayısı" && int.Parse(av.Value.ToString()) <= filter.MaxRooms.Value));
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(re => re.Price > filter.MinPrice || re.Price == filter.MinPrice);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(re => re.Price < filter.MaxPrice || re.Price == filter.MaxPrice);
        }

        return query;
    }
}