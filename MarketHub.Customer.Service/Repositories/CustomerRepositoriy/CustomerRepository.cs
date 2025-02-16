using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Collections;

namespace MarketHub.CustomerService.Repositories.CustomerRepositoriy;

public class CustomerRepository(IMongoDatabase database)
                                : GenericRepository<Customer>(database, nameof(Customer)), ICustomerRepository
{
    public async Task AddCustomerAddress(Guid customerId, Address address)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);

        var customerExists = await dbCollection.Find(filter).AnyAsync();

        if (!customerExists)
            throw new Exception("Customer not found.");

        var update = Builders<Customer>.Update.Push(c => c.Addresses, address);

        var result = await dbCollection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount == 0)
            throw new Exception("Failed to add address.");
    }

    public async Task AddCustomerAddresses(Guid customerId, List<Address> addresses)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);

        var customerExists = await dbCollection.Find(filter).AnyAsync();

        if (!customerExists)
            throw new Exception("Customer not found.");

        var update = Builders<Customer>.Update.PushEach(x => x.Addresses, addresses);

        var result = await dbCollection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount == 0)
            throw new Exception("Failed to add addresses.");
    }

    public async Task RemoveCustomerAddresses(Guid customerId,Guid addressId)
    {
        var filter = filterBuilder.Eq(c => c.Id, customerId);

        var customerExists = await dbCollection.Find(filter).FirstOrDefaultAsync();

        if (customerExists is null)
            throw new Exception("Customer not found.");

        var address = customerExists?.Addresses?.FirstOrDefault(x=>x.Id== addressId);

        if (address is null)
            throw new Exception("Address not found.");

        if (address.IsDeleted)
            throw new Exception("Address is already deleted.");

        address.IsDeleted = true;
        address.UpdatedAt = DateTime.UtcNow;

        var update = Builders<Customer>.Update.Set(c => c.Addresses, customerExists?.Addresses);

        var result = await dbCollection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount == 0)
            throw new Exception("Failed to update customer addresses.");

    }
}


