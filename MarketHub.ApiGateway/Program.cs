using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/identity/swagger/v1/swagger.json", "Identity API");
    options.SwaggerEndpoint("/product/swagger/v1/swagger.json", "Product API");

    options.RoutePrefix = "swagger"; // Swagger available at /swagger

});

app.UseHttpsRedirection();
app.UseRouting();

app.MapReverseProxy();

app.Run();
