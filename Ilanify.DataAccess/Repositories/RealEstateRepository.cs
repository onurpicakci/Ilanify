using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Dtos;
using Ilanify.DataAccess.EntityFramework;
using Ilanify.DataAccess.Extensions;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class RealEstateRepository : IRealEstateRepository
{
    private readonly IlanifyDbContext _context;
    
    public RealEstateRepository(IlanifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId)
    {
        return await _context.RealEstates
            .Where(re => re.CategoryId == categoryId)
            .Where(re => re.IsActive == true)
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
            .Where(x => x.IsActive == true)
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
            .Include(re => re.Category)
            .Include(re => re.AttributeValues) 
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.Images.OrderBy(i => i.Id).Take(1))
            .Where(re => re.Location.City == location)
            .Where(re => re.IsActive == true)
            .ToListAsync();
    }

    public async Task<RealEstate> GetRealEstateByIdWithDetailsAsync(int realEstateId)
    {
        var realEstate = await _context.RealEstates
            .Include(re => re.Images)
            .Include(re => re.Location)
            .Include(re => re.Category)
            .ThenInclude(c => c.CategoryAttributes)
            .Include(re => re.AttributeValues)
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.ApplicationUser)
            .FirstOrDefaultAsync(re => re.Id == realEstateId);

        if (realEstate == null)
        {
            return null;
        }
        
        return realEstate;
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByFilterAsync(RealEstateFilter filter)
    {
        var query = _context.RealEstates
            .Where(re => re.IsActive == true)
            .Include(re => re.Location)
            .Include(re => re.Category)
            .ThenInclude(c => c.CategoryAttributes)
            .Include(re => re.ApplicationUser)
            .Include(re => re.AttributeValues)
            .Include(re => re.Images)
            .AsQueryable();
    
        query = query.ApplyFilter(filter);

        return await query.ToListAsync();
    }


    public async Task<IEnumerable<RealEstate>> GetActiveRealEstatesByUserIdAsync(string userId)
    {
        return await _context.RealEstates
            .Include(re => re.Location)
            .Include(re => re.Category)
            .Include(re => re.Images)
            .Where(re => re.ApplicationUserId == userId)
            .Where(re => re.IsActive == true)
            .ToListAsync();
    }

    public async Task<int> GetRealEstatesCount()
    {
        return await _context.RealEstates.CountAsync();
    }

    public async Task<RealEstate> GetByIdAsync(int id)
    {
        return await _context.RealEstates
            .Include(re => re.Images)
            .Include(re => re.Location)
            .Include(re => re.Category)
            .ThenInclude(c => c.CategoryAttributes)
            .Include(re => re.AttributeValues)
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.ApplicationUser)
            .FirstOrDefaultAsync(re => re.Id == id);
    }

    public async Task<IEnumerable<RealEstate>> GetAllAsync()
    {
        return await _context.RealEstates
            .Include(re => re.Images)
            .Include(re => re.Location)
            .Include(re => re.Category)
            .ThenInclude(c => c.CategoryAttributes)
            .Include(re => re.AttributeValues)
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.ApplicationUser)
            .ToListAsync();
    }

    public Task AddAsync(RealEstate entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(RealEstate entity)
    {
        _context.RealEstates.Update(entity);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(RealEstate entity)
    {
        throw new NotImplementedException();
    }
}