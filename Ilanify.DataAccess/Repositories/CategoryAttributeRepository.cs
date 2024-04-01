using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class CategoryAttributeRepository : ICategoryAttributeRepository
{
    private readonly IlanifyDbContext _ilanifyDbContext;
    
    public CategoryAttributeRepository(IlanifyDbContext ilanifyDbContext)
    {
        _ilanifyDbContext = ilanifyDbContext;
    }

    public async Task<IEnumerable<CategoryAttribute>> GetAllCategoryAttributesAsync()
    {
        return await _ilanifyDbContext.CategoryAttributes
            .Include(x => x.Category)
            .ToListAsync();
    }
    public async Task<List<CategoryAttribute>> GetCategoryAttributesAsync(int categoryId)
    {
        return await _ilanifyDbContext.CategoryAttributes
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<CategoryAttribute> GetByIdAsync(int id)
    {
        return await _ilanifyDbContext.CategoryAttributes
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CategoryAttribute>> GetAllAsync()
    {
        return await _ilanifyDbContext.CategoryAttributes
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task AddAsync(CategoryAttribute entity)
    {
        await _ilanifyDbContext.CategoryAttributes.AddAsync(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(CategoryAttribute entity)
    {
        _ilanifyDbContext.CategoryAttributes.Update(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(CategoryAttribute entity)
    {
        _ilanifyDbContext.CategoryAttributes.Remove(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }
}