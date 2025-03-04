using MarketHub.Common.Library;
using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerModule.Api.DataTransferObjects;
using MarketHub.CustomerModule.Api.Entities;
using MarketHub.CustomerModule.Api.UnitOfWorks;
using System.Linq.Expressions;
using ZstdSharp.Unsafe;

namespace MarketHub.CustomerModule.Api.Services.CustomerServices;

public class CustomerBasicInfoService(IUnitOfWork unitOfWork) : ICustomerBasicInfoService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OperationResult> CreateCustomer(CreateCustomerBasicInfoDto customerBasicInfo)
    {
        var customer = new Customer
        {
            FirstName = customerBasicInfo.FirstName,
            LastName = customerBasicInfo.LastName,
            Email = customerBasicInfo.Email,
            PhoneNumber = customerBasicInfo.PhoneNumber,
            IsActive = true,
            Addresses = customerBasicInfo?.Addresses?.Select(x => new Address
            {
                Id = Guid.NewGuid(),
                Street = x.Street,
                City = x.City,
                State = x.State,
                ZipCode = x.ZipCode,
                CreatedAt = DateTime.UtcNow

            }).ToList()

        };
        await _unitOfWork.CustomerRepository.CreateAsync(customer);

        return OperationResult.Success();
    }
    public async Task<OperationResult> UpdateCustomer(Guid id,UpdateCustomerBasicInfoDto updateCustomerBasicInfo)
    {
        var existingCustomer = await _unitOfWork.CustomerRepository.GetAsync(id);

        if (existingCustomer is null)
            return Errors.ContentNotFound;

        existingCustomer.FirstName = updateCustomerBasicInfo.FirstName;
        existingCustomer.LastName = updateCustomerBasicInfo.LastName;
        existingCustomer.Email = updateCustomerBasicInfo.Email;
        existingCustomer.PhoneNumber = updateCustomerBasicInfo.PhoneNumber;

        await _unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);

        return OperationResult.Success();
    }
    public async Task<OperationResult> DeleteCustomer(Guid id)
    {
        await _unitOfWork.CustomerRepository.RemoveAsync(id);
        return OperationResult.Success();
    }
    public async Task<CustomerDetailsDto> GetCustomerBasicInfo(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetAsync(id);

        return new CustomerDetailsDto(customer.Id,
                                    customer.FirstName,
                                    customer.LastName,
                                    customer.Email,
                                    customer.PhoneNumber,
                                    customer?.Addresses?
                                                .Select(x=> new CustomerAddressDto(
                                                                x.Id,
                                                                x.Street,
                                                                x.City,
                                                                x.State,
                                                                x.ZipCode))
                                                .ToList());
    }
    public async Task<PaginationDto<CustomerBasicDto>> GetCustomerPagination(string? searchText, int pageNo, int size)
    {
        Expression<Func<Customer, bool>> filter = c => (searchText == null ||  c.FirstName.Contains(searchText)) && c.IsActive == true;

        var (customer,totalcount) = await _unitOfWork.CustomerRepository.GetPaginationAsync(filter, pageNo,size);

        var data = customer
                .Select(x => new CustomerBasicDto(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber))
                .ToList();

        return new PaginationDto<CustomerBasicDto>(totalcount,data);
    }

    public async Task<OperationResult> AddCustomerAddress(Guid customerId, List<CustomerAddressDto> addresses)
    {
        var customerAddress = addresses.Select(x => new Address
        {
            Id = Guid.NewGuid(),
            Street = x.Street ,
            City =x.City ,
            State =x.State ,
            ZipCode =x.ZipCode,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _unitOfWork.CustomerRepository.AddAddressAsync(customerId, customerAddress);

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCustomerAddress(Guid customerId,Guid addressId)
    {
        var isCustomerExist = await _unitOfWork.CustomerRepository.AnyAsync(c=> c.Id == customerId);

        if (!isCustomerExist)
            Errors.NewError(400,"Customer not available");


        await _unitOfWork.CustomerRepository.DeleteAddressAsync(customerId, addressId);
        return OperationResult.Success();
    }


    public Task<List<CustomerAddressDto>> GetCustomersAddresses(Guid customerId)
    {
        throw new NotImplementedException();
    }



    public Task<OperationResult> AddPaymentMethod(Guid customerId, List<PaymentMethodDto> paymentMethod)
    {
        throw new NotImplementedException();
    }
    public Task<OperationResult> DeletePaymentMethod(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PaymentMethodDto>> GetCustomersPaymentMethods(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdatePaymentMethod(List<PaymentMethodDto> paymentMethod)
    {
        throw new NotImplementedException();
    }

    public Task<List<CustomerBasicDto>> GetCustomersOfSpecificAddress(string address)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdateCustomer(UpdateCustomerBasicInfoDto updateCustomerBasicInfo)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpdateCustomerAddress(Guid customerId, CustomerAddressDto address)
    {
        throw new NotImplementedException();
    }
}
