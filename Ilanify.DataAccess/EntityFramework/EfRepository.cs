using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.EntityFramework;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly IlanifyDbContext _ilanifyDbContext;

    public EfRepository(IlanifyDbContext ilanifyDbContext)
    {
        _ilanifyDbContext = ilanifyDbContext;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _ilanifyDbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _ilanifyDbContext.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _ilanifyDbContext.Set<T>().AddAsync(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _ilanifyDbContext.Entry(entity).State = EntityState.Modified;
        await _ilanifyDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _ilanifyDbContext.Set<T>().Remove(entity);
        await _ilanifyDbContext.SaveChangesAsync();
    }
}