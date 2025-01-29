using MarketHub.Product.Service.Endpoints;
using MarketHub.Product.Service.Repositories.IServices;
using MarketHub.Product.Service.Repositories.Services;


namespace MarketHub.Product.Service;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IItemRepository, ItemRepository>();
        return services;
    }

    public static WebApplication UseApiEndpoients(this WebApplication app)
    {
        app.MapItemEndpoints();
        return app;
    }
}
