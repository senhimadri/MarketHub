using MarketHub.CustomerService.Repositories.CustomerRepositoriy;
using MongoDB.Driver;

namespace MarketHub.CustomerService.UnitOfWork;

public class UnitOfWork(IMongoDatabase database) : IUnitOfWork
{
    private readonly IMongoDatabase _database = database;
    private readonly ICustomerRepository? _customerRepository;

    public ICustomerRepository CustomerRepository
                                        => _customerRepository ?? new CustomerRepository(_database);

}
