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

    public async Task<AttributeValue> GetByIdAsync(int id)
    {
        return await _attributeValueRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(AttributeValue entity)
    {
        await _attributeValueRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(AttributeValue entity)
    {
        await _attributeValueRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(AttributeValue entity)
    {
        await _attributeValueRepository.DeleteAsync(entity);
    }
}