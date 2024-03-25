using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Dtos;
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

    public async Task<IEnumerable<CityRealEstateCount>> GetTop4CitiesByRealEstateCountAsync()
    {
        return await _realEstateRepository.GetTop4CitiesByRealEstateCountAsync();
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByLocationAsync(string location)
    {
        return await _realEstateRepository.GetRealEstatesByLocationAsync(location);
    }

    public async Task<RealEstate> GetRealEstateByIdWithDetailsAsync(int realEstateId)
    {
        return await _realEstateRepository.GetRealEstateByIdWithDetailsAsync(realEstateId);
    }

    public async Task<IEnumerable<RealEstate>> GetRealEstatesByFilterAsync(RealEstateFilter filter)
    {
        return await _realEstateRepository.GetRealEstatesByFilterAsync(filter);
    }

    public async Task<IEnumerable<RealEstate>> GetActiveRealEstatesByUserIdAsync(string userId)
    {
        return await _realEstateRepository.GetActiveRealEstatesByUserIdAsync(userId);
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