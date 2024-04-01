using Ilanify.Domain.Entities;

namespace Ilanify.DataAccess.Interfaces;

public interface IAttributeValueRepository : IRepository<AttributeValue>
{
    Task<IEnumerable<AttributeValue>> GetAllAttributeValuesAsync();
}