using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IlanifyDbContext _ilanifyDbContext;

    public CategoryRepository(IlanifyDbContext ilanifyDbContext)
    {
        _ilanifyDbContext = ilanifyDbContext;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _ilanifyDbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _ilanifyDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _ilanifyDbContext.Categories.ToListAsync();
    }

    public async Task AddAsync(Category entity)
    {
        await _ilanifyDbContext.Categories.AddAsync(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _ilanifyDbContext.Categories.Update(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category entity)
    {
        _ilanifyDbContext.Categories.Remove(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }
}