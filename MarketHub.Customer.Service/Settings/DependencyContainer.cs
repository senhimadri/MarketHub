using MarketHub.CustomerService.Repositories.CustomerRepositoriy;

namespace MarketHub.CustomerService.Settings;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        return services;
    }
}
