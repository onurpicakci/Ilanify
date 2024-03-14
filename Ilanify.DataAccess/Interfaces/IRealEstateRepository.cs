using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface IRealEstateRepository : IRepository<RealEstate>
{
    Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId);
    Task UploadImageAsync(RealEstateImage realEstateImage);
    Task<IEnumerable<IGrouping<string, RealEstate>>> GetRealEstatesGroupedByLocationAsync();
    Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location);
}