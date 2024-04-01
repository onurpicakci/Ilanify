using System.Collections;
using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface ICategoryAttributeRepository : IRepository<CategoryAttribute>
{
    Task<IEnumerable<CategoryAttribute>> GetAllCategoryAttributesAsync();
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId);
}