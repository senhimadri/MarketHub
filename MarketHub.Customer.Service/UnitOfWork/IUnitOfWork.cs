using MarketHub.CustomerService.Repositories.CustomerRepositoriy;

namespace MarketHub.CustomerService.UnitOfWork;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; }
}
