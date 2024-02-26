using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface IRealEstateService
{
    Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId);
    Task<RealEstate> GetByIdAsync(int id);
    Task<IEnumerable<RealEstate>> GetAllAsync();
    Task AddAsync(RealEstate entity);
    Task UpdateAsync(RealEstate entity);
    Task DeleteAsync(RealEstate entity);
}