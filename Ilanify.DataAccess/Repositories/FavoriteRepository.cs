using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly IlanifyDbContext _context;

    public FavoriteRepository(IlanifyDbContext context)
    {
        _context = context;
    }

    public async Task AddFavoriteAsync(string userId, int realEstateId)
    {
        var favorite = new Favorites
        {
            ApplicationUserId = userId,
            RealEstateId = realEstateId
        };

        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFavoriteAsync(string userId, int realEstateId)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.ApplicationUserId == userId && f.RealEstateId == realEstateId);

        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<RealEstate>> GetFavoritesAsync(string userId)
    {
        return await _context.Favorites
            .Where(f => f.ApplicationUserId == userId)
            .Include(f => f.RealEstate)
            .ThenInclude(r => r.Images)
            .Include(r => r.RealEstate)
            .ThenInclude(c => c.Category)
            .Include(re => re.RealEstate)
            .ThenInclude(t => t.Location)
            .Select(f => f.RealEstate)
            .ToListAsync();
    }

    public async Task<bool> IsFavoriteAsync(string userId, int realEstateId)
    {
        return await _context.Favorites
            .AnyAsync(f => f.ApplicationUserId == userId && f.RealEstateId == realEstateId);
    }
}