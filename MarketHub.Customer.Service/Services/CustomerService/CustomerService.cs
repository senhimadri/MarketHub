using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerService.DataTransferObjects;
using MarketHub.CustomerService.Entities;
using MarketHub.CustomerService.UnitOfWork;

namespace MarketHub.CustomerService.Services.CustomerService;

public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
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
    public Task<OperationResult> UpdateCustomer(UpdateCustomerBasicInfoDto updateCustomerBasicInfo)
    {
        throw new NotImplementedException();
    }
    public Task<OperationResult> DeleteCustomer(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<CustomerBasicDto> GetCustomerBasicInfo(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<List<CustomerBasicDto>> GetCustomerPagination(string searchText, int pageNo, int size)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> AddCustomerAddress(Guid customerId, List<CustomerAddressDto> addresses)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> DeleteCustomerAddress(Guid addressId)
    {
        throw new NotImplementedException();
    }
    public Task<OperationResult> UpdateCustomerAddress(List<CustomerAddressDto> addresses)
    {
        throw new NotImplementedException();
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
}
