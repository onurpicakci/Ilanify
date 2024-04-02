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

    public async Task<AttributeValue> GetByIdAsync(int id)
    {
        return await _context.AttributeValues
            .Include(x => x.RealEstate)
            .ThenInclude(x => x.Category)
            .Include(x => x.CategoryAttribute)
            .FirstOrDefaultAsync(x => x.AttributeValueId == id);
    }

    public async Task<IEnumerable<AttributeValue>> GetAllAsync()
    {
        return await _context.AttributeValues.ToListAsync();
    }

    public async Task AddAsync(AttributeValue entity)
    {
        await _context.AttributeValues.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AttributeValue entity)
    {
        _context.AttributeValues.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AttributeValue entity)
    {
        _context.AttributeValues.Remove(entity);
        await _context.SaveChangesAsync();
    }
}