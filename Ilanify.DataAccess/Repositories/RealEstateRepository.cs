using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Dtos;
using Ilanify.DataAccess.EntityFramework;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class RealEstateRepository : EfRepository<RealEstate>, IRealEstateRepository
{
    private readonly IlanifyDbContext _context;
    
    public RealEstateRepository(IlanifyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId)
    {
        return await _context.RealEstates
            .Where(re => re.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task UploadImageAsync(RealEstateImage realEstateImage)
    {
        await _context.RealEstateImages.AddAsync(realEstateImage);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CityRealEstateCount>> GetTop4CitiesByRealEstateCountAsync()
    {
        return await _context.RealEstates
            .Include(re => re.Location)
            .GroupBy(re => re.Location.City)
            .Select(group => new CityRealEstateCount 
            { 
                City = group.Key, 
                Count = group.Count() 
            })
            .OrderByDescending(x => x.Count)
            .Take(4)
            .ToListAsync();
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location)
    {
        return await _context.RealEstates
            .Include(re => re.Location)
            .Include(re => re.Images)
            .Include(re => re.Category)
            .Include(re => re.AttributeValues) 
            .ThenInclude(av => av.CategoryAttribute) 
            .Where(re => re.Location.City == location)
            .ToListAsync();
    }

}