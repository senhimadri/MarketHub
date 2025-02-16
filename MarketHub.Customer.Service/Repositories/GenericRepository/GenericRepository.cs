using MarketHub.CustomerService.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MarketHub.CustomerService.Repositories.GenericRepository;

public class GenericRepository<T>(IMongoDatabase database, string collectionName)
                                                    : IGenericRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);
    protected readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task CreateAsync(T entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(entity));

        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now;

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

    public async Task<(IReadOnlyCollection<T>,long)> GetPaginationAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize)
    {
        long totalCount = await dbCollection
                        .CountDocumentsAsync(filter);

        var datas = await dbCollection
                            .Find(filter)
                            .Skip((pageNumber - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();

        return (datas, totalCount);
    }

    public async Task<T> GetAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

        return await dbCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter )
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

        entity.UpdatedAt = DateTime.UtcNow;

        FilterDefinition<T> filter = filterBuilder.Eq(Existingentity => Existingentity.Id, entity.Id);
        await dbCollection.ReplaceOneAsync(filter, entity);
    }
}
