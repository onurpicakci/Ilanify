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
}