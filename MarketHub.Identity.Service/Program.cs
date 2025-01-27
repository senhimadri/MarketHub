using MarketHub.Identity.Service;
using MarketHub.Identity.Service.Endpoients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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