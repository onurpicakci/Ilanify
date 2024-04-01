using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}