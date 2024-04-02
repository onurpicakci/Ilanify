using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface ICategoryAttributeService
{
    Task<IEnumerable<CategoryAttribute>> GetAllCategoryAttributesAsync();
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId);
    Task AddAsync(CategoryAttribute entity);
    Task UpdateAsync(CategoryAttribute entity);
    Task DeleteAsync(CategoryAttribute entity);
    Task<CategoryAttribute> GetByIdAsync(int id);
}