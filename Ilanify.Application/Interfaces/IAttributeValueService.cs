using Ilanify.Domain.Entities;

namespace Ilanify.Application.Interfaces;

public interface IAttributeValueService
{
    Task<IEnumerable<AttributeValue>> GetAllAttributeValuesAsync();
}