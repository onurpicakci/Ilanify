using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Services;

public class CategoryAttributeService : ICategoryAttributeService
{
    private readonly ICategoryAttributeRepository _categoryAttributeRepository;

    public CategoryAttributeService(ICategoryAttributeRepository categoryAttributeRepository)
    {
        _categoryAttributeRepository = categoryAttributeRepository;
    }

    public async Task<IEnumerable<CategoryAttribute>> GetAllCategoryAttributesAsync()
    {
        return await _categoryAttributeRepository.GetAllCategoryAttributesAsync();
    }
    
    public async Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId)
    {
        return await _categoryAttributeRepository.GetCategoryAttributesAsync(categoryId);
    }

    public async Task AddAsync(CategoryAttribute entity)
    {
        await _categoryAttributeRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(CategoryAttribute entity)
    {
        await _categoryAttributeRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(CategoryAttribute entity)
    {
        await _categoryAttributeRepository.DeleteAsync(entity);
    }

    public async Task<CategoryAttribute> GetByIdAsync(int id)
    {
        return await _categoryAttributeRepository.GetByIdAsync(id);
    }
}