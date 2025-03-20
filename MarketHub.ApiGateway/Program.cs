using MarketHub.ApiGateway;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithExceptionDetails()
    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

//app.UseSerilogRequestLogging();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/identity/swagger/v1/swagger.json", "Identity API");
    options.SwaggerEndpoint("/product/swagger/v1/swagger.json", "Product API");
});

app.UseHttpsRedirection();
app.UseRouting();

app.MapReverseProxy();

app.UseRequestResponseLogging();

//Log.Information("🚀 MarketHub API Gateway started!");

app.Run();
