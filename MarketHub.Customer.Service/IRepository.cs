using MarketHub.Customer.Service.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MarketHub.Customer.Service;

public interface IRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T entity);
}

public class Repository<T>(IMongoDatabase database, string collectionName)
                                                    : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task CreateAsync(T entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(entity));

        await dbCollection.InsertOneAsync(entity);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await dbCollection.Find(filter: filterBuilder.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await dbCollection.Find(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

        return await dbCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await dbCollection
                .Find(filter)
                .FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var filter = filterBuilder.Eq(existingentity => existingentity.Id, id);
        await dbCollection.DeleteOneAsync(filter);
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        FilterDefinition<T> filter = filterBuilder.Eq(Existingentity => Existingentity.Id, entity.Id);
        await dbCollection.ReplaceOneAsync(filter, entity);
    }
}
