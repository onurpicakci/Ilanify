using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface IRealEstateRepository : IRepository<RealEstate>
{
    Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId);
}