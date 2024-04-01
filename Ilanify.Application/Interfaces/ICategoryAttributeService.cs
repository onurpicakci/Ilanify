using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface ICategoryAttributeService
{
    Task<IEnumerable<CategoryAttribute>> GetAllCategoryAttributesAsync();
    Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId);
}