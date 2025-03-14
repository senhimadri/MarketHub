﻿using MarketHub.CustomerModule.Api.Entities;
using System.Linq.Expressions;

namespace MarketHub.CustomerModule.Api.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<(IReadOnlyCollection<T>, long)> GetPaginationAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}
