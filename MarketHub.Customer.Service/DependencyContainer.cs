using MarketHub.CustomerModule.Api.Enpoients;
using MarketHub.CustomerModule.Api.Services.CustomerServices;
using MarketHub.CustomerModule.Api.UnitOfWorks;

namespace MarketHub.CustomerModule.Api;

public static class DependencyContainer 
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerBasicInfoService, CustomerBasicInfoService>();
        return services;
    }

    public static WebApplication UseApiEndpoients(this WebApplication app)
    {
        app.MapCustomerEndpoints();
        app.MapCustomersAddressEndpoints();
        return app;
    }
}
