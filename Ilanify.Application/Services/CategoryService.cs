using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetCategoriesAsync();
    }

    public async Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId)
    {
        return await _categoryRepository.GetCategoryAttributesAsync(categoryId);
    }
}