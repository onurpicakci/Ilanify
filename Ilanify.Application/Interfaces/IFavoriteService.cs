using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface IFavoriteService
{
    Task AddFavoriteAsync(string userId, int realEstateId);
    Task RemoveFavoriteAsync(string userId, int realEstateId);
    Task<IEnumerable<RealEstate>> GetFavoritesAsync(string userId);
    Task<bool> IsFavoriteAsync(string userId, int realEstateId);
}