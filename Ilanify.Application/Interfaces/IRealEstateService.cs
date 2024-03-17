using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface IRealEstateService
{
    Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId);
    Task UploadImageAsync(RealEstateImage realEstateImage);
    Task<IEnumerable<CityRealEstateCount>> GetRealEstatesGroupedByLocationAsync();
    Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location);
    Task<RealEstate> GetByIdAsync(int id);
    Task<IEnumerable<RealEstate>> GetAllAsync();
    Task AddAsync(RealEstate entity);
    Task UpdateAsync(RealEstate entity);
    Task DeleteAsync(RealEstate entity);
}