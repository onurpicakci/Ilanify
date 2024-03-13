using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Services;

public class RealEstateService : IRealEstateService
{
    private readonly IRealEstateRepository _realEstateRepository;

    public RealEstateService(IRealEstateRepository realEstateRepository)
    {
        _realEstateRepository = realEstateRepository;
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByCategoryAsync(int categoryId)
    {
        return await _realEstateRepository.GetRealEstatesByCategoryAsync(categoryId);
    }

    public async Task UploadImageAsync(RealEstateImage realEstateImage)
    {
        await _realEstateRepository.UploadImageAsync(realEstateImage);
    }

    public async Task<IEnumerable<IGrouping<int, RealEstate>>> GetRealEstatesGroupedByLocationAsync()
    {
        return await _realEstateRepository.GetRealEstatesGroupedByLocationAsync();
    }

    public async Task<RealEstate> GetByIdAsync(int id)
    {
        return await _realEstateRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<RealEstate>> GetAllAsync()
    {
        return await _realEstateRepository.GetAllAsync();
    }

    public async Task AddAsync(RealEstate entity)
    {
        await _realEstateRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(RealEstate entity)
    {
        await _realEstateRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(RealEstate entity)
    {
        await _realEstateRepository.DeleteAsync(entity);
    }
}