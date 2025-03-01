﻿using MarketHub.CustomerService.Entities;
using System.Linq.Expressions;

namespace MarketHub.CustomerService.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T entity);
}
