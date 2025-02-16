using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.Repositories.GenericRepository;

namespace MarketHub.CustomerService.Repositories.CustomerRepositoriy;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task AddCustomerAddress(Guid customerId, Address address);
    Task AddCustomerAddresses(Guid customerId, List<Address> addresses);
}
