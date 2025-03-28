﻿using MarketHub.ProductModule.Api.Entities;
using System.Linq.Expressions;

namespace MarketHub.ProductModule.Api.Repositories.IServices;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
