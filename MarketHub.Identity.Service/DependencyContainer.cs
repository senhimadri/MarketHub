using MarketHub.IdentityModule.Api.Endpoients;
using MarketHub.IdentityModule.Api.Repositories.Login;
using MarketHub.IdentityModule.Api.Repositories.Registration;
using MarketHub.IdentityModule.Api.Repositories.Token;

namespace MarketHub.IdentityModule.Api;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenUtils, TokenUtils>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<ILoginService, LoginService>();
        return services;
    }

    public static WebApplication UseApiEndpoients(this WebApplication app)
    {
        app.MapUserEndpoints();
        app.MapLoginEndpoints();
        return app;
    }
}
