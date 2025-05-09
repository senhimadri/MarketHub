using MarketHub.Common.Library.MassTransit;
using MarketHub.ProductModule.Api;
using MarketHub.ProductModule.Api.Endpoints;
using MarketHub.ProductModule.Api.Repositories.IServices;
using MarketHub.ProductModule.Api.Repositories.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransitConfiguration();

builder.Services.AddTransient<IItemRepository, ItemRepository>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapItemEndpoints();

app.Run();