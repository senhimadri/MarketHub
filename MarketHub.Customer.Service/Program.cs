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

app.MapCustomerEndpoints();
app.MapCustomersAddressEndpoints();

app.Run();
