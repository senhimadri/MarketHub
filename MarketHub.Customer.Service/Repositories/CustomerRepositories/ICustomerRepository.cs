using MarketHub.CustomerModule.Api.Entities;
using MarketHub.CustomerModule.Api.Repositories.GenericRepository;

namespace MarketHub.CustomerModule.Api.Repositories.CustomerRepositories;

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
