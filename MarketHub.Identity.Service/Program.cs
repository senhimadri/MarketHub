using MarketHub.IdentityModule.Api;
using MarketHub.IdentityModule.Api.DataTransferObjects;
using MarketHub.IdentityModule.Api.Endpoients;
using MarketHub.IdentityModule.Api.Repositories.Login;
using MarketHub.IdentityModule.Api.Repositories.Registration;
using Microsoft.EntityFrameworkCore;
using MarketHub.Common.Library.OperationResult;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.MapUserEndpoints();
app.MapLoginEndpoints();
app.MapPost("/register", async (IRegistrationService registrationService, CreateIdentityUserDto user) =>
{
    var response = await registrationService.RegisterUserAsync(user);

    return response.Match(onSuccess: () => Results.Ok("User created successfully"),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.MapPost("/login", async (ILoginService loginService, LoginDto request) =>
{
    var response = await loginService.LoginAsync(request);
    return response is not null ? Results.Ok(response) : Results.NotFound();
});

app.MapPost("/refresh-token", async (ILoginService loginService, string refreshToken) =>
{
    var response = await loginService.RefreshTokenAsync(refreshToken);
    return response is not null ? Results.Ok(response) : Results.NotFound();
});

app.Run();