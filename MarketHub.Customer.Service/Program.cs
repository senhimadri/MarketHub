using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerService.DataTransferObjects;
using MarketHub.CustomerService.Repositories.CustomerRepositoriy;
using MarketHub.CustomerService.Services.CustomerService;
using MarketHub.CustomerService.Settings;
using MarketHub.CustomerService.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongo();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService,CustomerService>();




var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("/CreateCustomer", async (ICustomerService _customerService, [FromBody] CreateCustomerBasicInfoDto request) =>
{
    var response = await _customerService.CreateCustomer(request);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.Run();
