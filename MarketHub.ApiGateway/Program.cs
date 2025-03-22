using MarketHub.ApiGateway;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    //.Enrich.WithExceptionDetails()
    .CreateLogger();

builder.Host.UseSerilog((context, services, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

//app.UseSerilogRequestLogging();

//app.UseSwagger();

app.UseHttpsRedirection();
app.UseRouting();

app.UseRequestResponseLogging();



app.MapReverseProxy();

app.Run();
