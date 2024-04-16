using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface IFavoriteRepository
{
    Task AddFavoriteAsync(string userId, int realEstateId);
    Task RemoveFavoriteAsync(string userId, int realEstateId);
    Task<IEnumerable<RealEstate>> GetFavoritesAsync(string userId);
    Task<bool> IsFavoriteAsync(string userId, int realEstateId);
}