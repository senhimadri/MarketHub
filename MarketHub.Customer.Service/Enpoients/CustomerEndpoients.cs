using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerModule.Api.DataTransferObjects;
using MarketHub.CustomerModule.Api.Services.CustomerServices;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.CustomerModule.Api.Enpoients;

public static class CustomerEndpoients
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customer").WithTags("Customer");

        group.MapPost("/create", async (ICustomerBasicInfoService _customerService, [FromBody] CreateCustomerBasicInfoDto request) =>
        {
            var response = await _customerService.CreateCustomer(request);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Create");

        group.MapPut("/update", async (ICustomerBasicInfoService _customerService, [FromBody] UpdateCustomerBasicInfoDto request) =>
        {
            var response = await _customerService.UpdateCustomer(request);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Update");

        group.MapPut("/delete/{id}", async (ICustomerBasicInfoService _customerService, Guid id) =>
        {
            var response = await _customerService.DeleteCustomer(id);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Delete");

        group.MapGet("/getby/{id}", async (ICustomerBasicInfoService _customerService, Guid id) =>
        {
            var response = await _customerService.GetCustomerBasicInfo(id);
            return response is not null ? Results.Ok(response) : Results.NoContent();
        }).WithName("GetbyId"); ;

        group.MapGet("/pagination", async (ICustomerBasicInfoService _customerService, string? searchText, int pageNo, int size) =>
        {
            var data = await _customerService.GetCustomerPagination(searchText, pageNo, size);

            return Results.Ok(data);
        }).WithName("Pagination");

        return app;
    }
    public static IEndpointRouteBuilder MapCustomersAddressEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customeraddress").WithTags("Customer Address");

        group.MapPut("/add/{customerId}", async (ICustomerBasicInfoService _customerService, [FromHeader] Guid customerId, [FromBody] List<CustomerAddressDto> addresses) =>
        {
            var response = await _customerService.AddCustomerAddress(customerId, addresses);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Add Customers Address");

        group.MapPut("/update/{customerId}", async (ICustomerBasicInfoService _customerService, [FromHeader] Guid customerId, [FromBody] CustomerAddressDto address) =>
        {
            var response = await _customerService.UpdateCustomerAddress(customerId, address);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Update Customers Address");

        group.MapDelete("/delete/{customerId}/{addressId}", async (ICustomerBasicInfoService _customerService, [FromHeader] Guid customerId, [FromHeader] Guid addressId) =>
        {
            var response = await _customerService.DeleteCustomerAddress(customerId, addressId);
            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        }).WithName("Delete Customers Address");

        return app;
    }
}
