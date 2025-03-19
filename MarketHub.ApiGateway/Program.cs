using MarketHub.ApiGateway;
using Serilog;
using Serilog.Sinks.Network;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        //.Enrich.WithProperty("Service", "MarketHub-ApiGateway") // Add service name for better filtering
        .WriteTo.Console();
        //.WriteTo.TCPSink("tcp://localhost:5044"); // Use host & port directly
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRequestResponseLogging();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/identity/swagger/v1/swagger.json", "Identity API");
    options.SwaggerEndpoint("/product/swagger/v1/swagger.json", "Product API");
});

app.UseHttpsRedirection();
app.UseRouting();

app.MapReverseProxy();

app.Run();
