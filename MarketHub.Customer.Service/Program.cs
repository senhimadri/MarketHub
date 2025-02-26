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

app.MapPut("/UpdateCustomer", async (ICustomerService _customerService, [FromBody] UpdateCustomerBasicInfoDto request) =>
{
    var response = await _customerService.UpdateCustomer(request);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.MapPut("/DeleteCustomer/{id}", async (ICustomerService _customerService, Guid id) =>
{
    var response = await _customerService.DeleteCustomer(id);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.MapGet("/GetCustomer/{id}", async (ICustomerService _customerService , Guid id) =>
{
    var response = await _customerService.GetCustomerBasicInfo(id);
    return response is not null ? Results.Ok(response) : Results.NoContent();
});

app.MapGet("/GetCustomersPagination", async (ICustomerService _customerService, string? searchText, int pageNo, int size) =>
{
    var (data, totalCount) = await _customerService.GetCustomerPagination(searchText, pageNo, size);

    return Results.Ok(data);
});


app.MapPut("/AddCustomerAddresses/{customerId}", async (ICustomerService _customerService,[FromHeader] Guid customerId,[FromBody]List<CustomerAddressDto> addresses) =>
{
    var response = await _customerService.AddCustomerAddress(customerId, addresses);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.MapPut("/UpdateCustomerAddresses/{customerId}", async (ICustomerService _customerService, [FromHeader] Guid customerId, [FromBody] CustomerAddressDto address) =>
{
    var response = await _customerService.UpdateCustomerAddress(customerId, address);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.MapDelete("/UpdateCustomerAddresses/{customerId}/{addressId}", async (ICustomerService _customerService, [FromHeader] Guid customerId, [FromHeader] Guid addressId) =>
{
    var response = await _customerService.DeleteCustomerAddress(customerId, addressId);

    return response.Match(onSuccess: () => Results.Created(),
                onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                onFailure: (error) => Results.Problem(error.Description));
});

app.Run();
