using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class AttributeValueRepository : IAttributeValueRepository
{
    private readonly IlanifyDbContext _context;

    public AttributeValueRepository(IlanifyDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<AttributeValue>> GetAllAttributeValuesAsync()
    {
        return await _context.AttributeValues
            .Include(x => x.RealEstate)
            .Include(x => x.CategoryAttribute)
            .ToListAsync();
    }

    public Task<AttributeValue> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AttributeValue>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AttributeValue entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AttributeValue entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AttributeValue entity)
    {
        throw new NotImplementedException();
    }
}