using MarketHub.CustomerModule.Api.Repositories.CustomerRepositories;

namespace MarketHub.CustomerModule.Api.UnitOfWorks;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
}
