using Ilanify.DataAccess.Context;
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

    public async Task<IEnumerable<IGrouping<string, RealEstate>>> GetRealEstatesGroupedByLocationAsync()
    {
        return await _context.RealEstates
            .Include(re => re.Location)
            .GroupBy(re => re.Location.City)
            .ToListAsync();
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location)
    {
        return await _context.RealEstates
            .Include(re => re.Location)
            .Include(re => re.Images)
            .Where(re => re.Location.City == location)
            .ToListAsync();
    }
}