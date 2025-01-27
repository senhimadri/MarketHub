using MarketHub.Identity.Service.Repositories.Login;
using MarketHub.Identity.Service.Repositories.Registration;
using MarketHub.Identity.Service.Repositories.Token;

namespace MarketHub.Identity.Service;

public static class DependencyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenUtils, TokenUtils>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<ILoginService, LoginService>();
        return services;
    }
}
