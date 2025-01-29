using MarketHub.Product.Service;
using MarketHub.Product.Service.Endpoints;
using MarketHub.Product.Service.Repositories.IServices;
using MarketHub.Product.Service.Repositories.Services;
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


app.UseApiEndpoients();
app.Run();