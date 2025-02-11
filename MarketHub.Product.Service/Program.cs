using MarketHub.Product.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IItemRepository, ItemRepository>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapItemEndpoints();
app.Run();