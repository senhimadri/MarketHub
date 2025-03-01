using MarketHub.CustomerModule.Api.Repositories.CustomerRepositories;
using MongoDB.Driver;

namespace MarketHub.CustomerModule.Api.UnitOfWorks;

public class UnitOfWork(IMongoDatabase database) : IUnitOfWork
{
    private readonly IMongoDatabase _database = database;
    private readonly ICustomerRepository? _customerRepository;

    public ICustomerRepository CustomerRepository
                                        => _customerRepository ?? new CustomerRepository(_database);

}
