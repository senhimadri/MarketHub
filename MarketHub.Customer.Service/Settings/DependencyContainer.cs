using MarketHub.CustomerModule.Api.Repositories.CustomerRepositories;

namespace MarketHub.CustomerModule.Api.Settings;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        return services;
    }
}
