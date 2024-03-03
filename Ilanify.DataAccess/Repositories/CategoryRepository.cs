using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.EntityFramework;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Repositories;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    private readonly IlanifyDbContext _ilanifyDbContext;
    
    public CategoryRepository(IlanifyDbContext ilanifyDbContext) : base(ilanifyDbContext)
    {
        _ilanifyDbContext = ilanifyDbContext;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _ilanifyDbContext.Categories.ToListAsync();
    }
}