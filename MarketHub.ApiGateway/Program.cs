var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagg


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
