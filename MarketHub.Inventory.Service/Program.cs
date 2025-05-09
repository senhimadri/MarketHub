using MarketHub.Common.Library.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();