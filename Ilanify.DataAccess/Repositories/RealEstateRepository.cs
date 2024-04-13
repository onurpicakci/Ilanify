using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Dtos;
using Ilanify.DataAccess.EntityFramework;
using Ilanify.DataAccess.Extensions;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Ilanify.Domain.Enums;
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
            .Include(re => re.Location)
            .Include(re => re.Category)
            .Include(re => re.AttributeValues)
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.Images.OrderBy(i => i.Id).Take(1))
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

    public async Task<IEnumerable<RealEstate>> SearchRealEstatesAsync(RealEstateSearchQuery searchFilter)
    {
        var query = _context.RealEstates
            .Where(re => re.IsActive == true)
            .Include(re => re.Location)
            .Include(re => re.Category)
            .ThenInclude(c => c.CategoryAttributes)
            .Include(re => re.ApplicationUser)
            .Include(re => re.AttributeValues)
            .Include(re => re.Images.OrderBy(i => i.Id).Take(1))
            .AsQueryable();

        return await query.SearchRealEstatesAsync(searchFilter);
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByUserIdAsync(string userId, bool isActive = true)
    {
        IQueryable<RealEstate> query = _context.RealEstates
            .Include(re => re.Location)
            .Include(re => re.Category)
            .Include(re => re.Images)
            .Where(re => re.ApplicationUserId == userId);

        if (isActive)
        {
            query = query.Where(re => re.IsActive == true);
        }
        else
        {
            query = query.Where(re => re.IsActive == false);
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetRealEstatesCount()
    {
        return await _context.RealEstates.CountAsync();
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByTypeAsync(RealEstateType realEstateType)
    {
        RealEstateType typeEnum = (RealEstateType)Enum.Parse(typeof(RealEstateType), realEstateType.ToString());

        return await _context.RealEstates
            .Include(re => re.Location)
            .Include(re => re.Category)
            .Include(re => re.AttributeValues)
            .ThenInclude(av => av.CategoryAttribute)
            .Include(re => re.Images.OrderBy(i => i.Id).Take(1))
            .Where(re => re.IsActive == true)
            .Where(re => re.Type == typeEnum)
            .ToListAsync();
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

    public async Task AddAsync(RealEstate entity)
    {
        await _context.RealEstates.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RealEstate entity)
    {
        _context.RealEstates.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RealEstate entity)
    {
        _context.RealEstates.Remove(entity);
        await _context.SaveChangesAsync();
    }
}