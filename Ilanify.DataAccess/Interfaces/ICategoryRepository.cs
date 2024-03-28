using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId);
}