using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;

namespace Ilanify.Application.Services;

public class AttributeValueService : IAttributeValueService
{
    private readonly IAttributeValueRepository _attributeValueRepository;

    public AttributeValueService(IAttributeValueRepository attributeValueRepository)
    {
        _attributeValueRepository = attributeValueRepository;
    }

    public async Task<IEnumerable<AttributeValue>> GetAllAttributeValuesAsync()
    {
        return await _attributeValueRepository.GetAllAttributeValuesAsync();
    }
}