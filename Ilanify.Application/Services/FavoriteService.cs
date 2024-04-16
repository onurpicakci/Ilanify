using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task AddFavoriteAsync(string userId, int realEstateId)
    {
        await _favoriteRepository.AddFavoriteAsync(userId, realEstateId);
    }

    public async Task RemoveFavoriteAsync(string userId, int realEstateId)
    {
        await _favoriteRepository.RemoveFavoriteAsync(userId, realEstateId);
    }

    public async Task<IEnumerable<RealEstate>> GetFavoritesAsync(string userId)
    {
        return await _favoriteRepository.GetFavoritesAsync(userId);
    }

    public async Task<bool> IsFavoriteAsync(string userId, int realEstateId)
    {
        return await _favoriteRepository.IsFavoriteAsync(userId, realEstateId);
    }
}