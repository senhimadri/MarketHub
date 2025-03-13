using MarketHub.Common.Library.OperationResult;
using MarketHub.IdentityModule.Api.DataTransferObjects;
using MarketHub.IdentityModule.Api.Repositories.Login;
using MarketHub.IdentityModule.Api.Repositories.Registration;

namespace MarketHub.IdentityModule.Api.Endpoients;

public static class UserRegistrationEndpoint
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").WithTags("Users");

        group.MapPost("/", async (IRegistrationService registrationService, CreateIdentityUserDto user) =>
        {
            var response = await registrationService.RegisterUserAsync(user);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("CreateUser");

        return app;
    }

    public static IEndpointRouteBuilder MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        group.MapPost("/login", async (ILoginService loginService, LoginDto request) =>
        {
            var response = await loginService.LoginAsync(request);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }).WithName("LoginUser");

        group.MapPost("/refresh-token", async (ILoginService loginService, string refreshToken) =>
        {
            var response = await loginService.RefreshTokenAsync(refreshToken);
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }).WithName("RefreshAccessToken");

        return app;
    }
}
