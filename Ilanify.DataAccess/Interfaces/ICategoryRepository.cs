using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}