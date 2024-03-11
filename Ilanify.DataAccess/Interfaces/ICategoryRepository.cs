using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId);
}