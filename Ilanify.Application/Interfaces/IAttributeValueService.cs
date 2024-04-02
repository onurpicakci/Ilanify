using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface IAttributeValueService
{
    Task<IEnumerable<AttributeValue>> GetAllAttributeValuesAsync();
    Task<AttributeValue> GetByIdAsync(int id);
    Task AddAsync(AttributeValue entity);
    Task UpdateAsync(AttributeValue entity);
    Task DeleteAsync(AttributeValue entity);
    
}