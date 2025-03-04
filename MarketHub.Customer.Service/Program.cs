using MarketHub.CustomerModule.Api.Enpoients;
using MarketHub.CustomerModule.Api.Services.CustomerServices;
using MarketHub.CustomerModule.Api.Settings;
using MarketHub.CustomerModule.Api.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongo();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerBasicInfoService,CustomerBasicInfoService>();




var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapCustomerEndpoints();
app.MapCustomersAddressEndpoints();

app.Run();
