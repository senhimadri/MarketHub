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

app.MapUserEndpoints();
app.MapLoginEndpoints();

app.Run();