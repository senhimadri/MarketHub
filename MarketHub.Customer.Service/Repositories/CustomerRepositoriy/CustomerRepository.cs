using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.Repositories.GenericRepository;
using MongoDB.Driver;

namespace MarketHub.CustomerService.Repositories.CustomerRepositoriy;

public class CustomerRepository(IMongoDatabase database, string collectionName)
                                : GenericRepository<Customer>(database, collectionName), ICustomerRepository;

