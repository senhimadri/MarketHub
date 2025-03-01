using MarketHub.Common.Library;
using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerModule.Api.DataTransferObjects;

namespace MarketHub.CustomerModule.Api.Services.CustomerServices;

public interface ICustomerBasicInfoService
{
    Task<OperationResult> CreateCustomer(CreateCustomerBasicInfoDto createCustomerBasicInfo);
    Task<OperationResult> UpdateCustomer(UpdateCustomerBasicInfoDto updateCustomerBasicInfo);
    Task<OperationResult> DeleteCustomer(Guid id);
    Task<CustomerDetailsDto> GetCustomerBasicInfo(Guid id);
    Task<PaginationDto<CustomerBasicDto>> GetCustomerPagination(string? searchText, int pageNo, int size);

 
    Task<OperationResult> AddCustomerAddress(Guid customerId, List<CustomerAddressDto> addresses);
    Task<OperationResult> UpdateCustomerAddress(Guid customerId, CustomerAddressDto address);
    Task<OperationResult> DeleteCustomerAddress(Guid customerId, Guid addressId);
    Task<List<CustomerAddressDto>> GetCustomersAddresses(Guid customerId);

    Task<OperationResult> AddPaymentMethod(Guid customerId, List<PaymentMethodDto> paymentMethod);
    Task<OperationResult> UpdatePaymentMethod(List<PaymentMethodDto> paymentMethod);
    Task<OperationResult> DeletePaymentMethod(Guid Id);
    Task<List<PaymentMethodDto>> GetCustomersPaymentMethods(Guid customerId);

    Task<List<CustomerBasicDto>> GetCustomersOfSpecificAddress(string address);
}
