using MarketHub.CustomerModule.Api.Entities;
using MarketHub.CustomerModule.Api.Repositories.GenericRepository;
using MongoDB.Driver;

namespace MarketHub.CustomerModule.Api.Repositories.CustomerRepositories;

public class CustomerRepository(IMongoDatabase database)
                                : GenericRepository<Customer>(database, nameof(Customer)), ICustomerRepository
{
    public async Task AddAddressAsync(Guid customerId, Address address)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);
        var update = Builders<Customer>.Update.Push(c => c.Addresses, address);
        await dbCollection.UpdateOneAsync(filter , update);
    }
    public async Task AddAddressAsync(Guid customerId, List<Address> addresses)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);
        var update = Builders<Customer>.Update.PushEach(c => c.Addresses, addresses);
        await dbCollection.UpdateOneAsync(filter, update);
    }

    public async Task UpdateAddressAsync(Guid customerId, Address address)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId) 
            & filterBuilder.ElemMatch(c => c.Addresses, a => a.Id == address.Id);

        var update = Builders<Customer>.Update.Set("Addresses.$[elem]", address);

        await dbCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAddressAsync(Guid customerId, Guid addressId)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);

        var update = Builders<Customer>.Update.PullFilter(c => c.Addresses, a => a.Id == addressId);

        await dbCollection.UpdateOneAsync(filter, update);
    }

    public async Task AddPaymentMethodAsync(Guid customerId, PaymentMethod paymentMethod)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);
        var update = Builders<Customer>.Update.Push(c => c.PaymentMethods, paymentMethod);
        await dbCollection.UpdateOneAsync(filter,update);

    }



    public Task DeletePaymentMethodAsync(Guid customerId, Guid paymentMethodId)
    {
        throw new NotImplementedException();
    }



    public Task UpdatePaymentMethodAsync(Guid customerId, PaymentMethod paymentMethod)
    {
        throw new NotImplementedException();
    }
}


