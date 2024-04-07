using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;
using Ilanify.Domain.Enums;

namespace Ilanify.DataAccess.Interfaces;

public interface IRealEstateRepository : IRepository<RealEstate>
{
    Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId);
    Task UploadImageAsync(RealEstateImage realEstateImage);
    Task<IEnumerable<CityRealEstateCount>> GetTop4CitiesByRealEstateCountAsync();
    Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location);
    Task<RealEstate> GetRealEstateByIdWithDetailsAsync(int realEstateId); 
    Task<IEnumerable<RealEstate>> GetRealEstatesByFilterAsync(RealEstateFilter filter);
    Task<IEnumerable<RealEstate>> GetActiveRealEstatesByUserIdAsync(string userId);
    Task<int> GetRealEstatesCount();
    Task<IEnumerable<RealEstate>> GetRealEstatesByTypeAsync(RealEstateType realEstateType);
}