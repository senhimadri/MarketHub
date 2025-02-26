using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.Repositories.GenericRepository;

namespace MarketHub.CustomerService.Repositories.CustomerRepositoriy;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task AddAddressAsync(Guid customerId, Address address);
    Task AddAddressAsync(Guid customerId, List<Address> addresses);
    Task UpdateAddressAsync(Guid customerId, Address address);
    Task DeleteAddressAsync(Guid customerId, Guid addressId);

    Task AddPaymentMethodAsync(Guid customerId, PaymentMethod paymentMethod);
    Task UpdatePaymentMethodAsync(Guid customerId, PaymentMethod paymentMethod);
    Task DeletePaymentMethodAsync(Guid customerId, Guid paymentMethodId);
}
