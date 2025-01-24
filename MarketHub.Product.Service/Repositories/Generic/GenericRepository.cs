using MarketHub.Product.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketHub.Product.Service.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
    }
}
