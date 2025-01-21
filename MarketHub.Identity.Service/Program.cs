using MarketHub.Identity.Service;
using MarketHub.Identity.Service.DataTransferObjects;
using MarketHub.Identity.Service.Repositories.Login;
using MarketHub.Identity.Service.Repositories.Registration;
using MarketHub.Identity.Service.Repositories.Token;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TokenUtils>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ILoginService, LoginService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/register", async (IRegistrationService registrationService, CreateIdentityUserDto user) =>
{
    await registrationService.RegisterUserAsync(user);
    return Results.Created();
});

app.MapPost("/login", async (ILoginService loginService, LoginDto request) =>
{
    var response = await loginService.LoginAsync(request);
    return Results.Ok(response);
});

app.MapPost("/refresh-token", async (ILoginService loginService, string refreshToken) =>
{
    var response = await loginService.RefreshTokenAsync(refreshToken);
    return Results.Ok(response);
});

app.Run();