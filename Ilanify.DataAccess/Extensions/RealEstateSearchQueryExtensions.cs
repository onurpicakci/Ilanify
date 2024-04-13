using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Extensions;

public static class RealEstateSearchQueryExtensions
{
    public static async Task<IEnumerable<RealEstate>> SearchRealEstatesAsync(this IQueryable<RealEstate> query, RealEstateSearchQuery searchQuery)
    {
        if (searchQuery == null)
        {
            throw new ArgumentNullException(nameof(searchQuery));
        }

        if (!string.IsNullOrEmpty(searchQuery.SearchText))
        {
            var searchTerms = searchQuery.SearchText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var term in searchTerms)
            {
                query = query.Where(re => re.Title.Contains(term)
                                          || re.Location.City.Contains(term)
                                          || re.Location.District.Contains(term)
                                          || re.Location.Neighborhood.Contains(term)
                                          || re.Id.ToString().Contains(term)
                );
            }
        }

        return await query.ToListAsync();
    }

}