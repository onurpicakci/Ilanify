using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Extensions;

public static class RealEstateFilterQueryExtensions
{
    public static IQueryable<RealEstate> ApplyFilter(this IQueryable<RealEstate> query, RealEstateFilter filter)
    {
        if (filter.CategoryId.HasValue)
        {
            query = query.Where(re => re.CategoryId == filter.CategoryId);
        }

        if (!string.IsNullOrEmpty(filter.City))
        {
            query = query.Where(re => re.Location.City.Contains(filter.City));
        }

        if (!string.IsNullOrEmpty(filter.District))
        {
            query = query.Where(re => re.Location.District.Contains(filter.District));
        }
        
        if (!string.IsNullOrEmpty(filter.Neighborhood))
        {
            query = query.Where(re => re.Location.Neighborhood.Contains(filter.Neighborhood));
        }

        if (!string.IsNullOrEmpty(filter.RoomCount))
        {
            query = query.Where(re => re.AttributeValues
                .Any(av => av.CategoryAttribute.Name == "Oda Sayısı" && av.Value == filter.RoomCount));    
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(re => re.Price > filter.MinPrice);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(re => re.Price < filter.MaxPrice);
        }
        
        if (filter.MinSquareMeters.HasValue)
        {
            query = query.Where(re => re.SquareMeters > filter.MinSquareMeters);
        }
        
        if (filter.MaxSquareMeters.HasValue)
        {
            query = query.Where(re => re.SquareMeters < filter.MaxSquareMeters);
        }
        
        if (filter.RealEstateType.HasValue)
        {
            query = query.Where(re => re.Type == filter.RealEstateType);
        }
        
        return query;
    }
}