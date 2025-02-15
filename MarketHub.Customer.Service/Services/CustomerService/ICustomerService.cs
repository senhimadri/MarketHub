using MarketHub.Common.Library.OperationResult;
using MarketHub.CustomerService.DataTransferObjects;

namespace MarketHub.CustomerService.Services.CustomerService;

public interface ICustomerService
{
    Task<OperationResult> CreateCustomer(CreateCustomerBasicInfoDto createCustomerBasicInfo);
    Task<OperationResult> UpdateCustomer(UpdateCustomerBasicInfoDto updateCustomerBasicInfo);
    Task<OperationResult> DeleteCustomer(Guid id);
    Task<CustomerBasicDto> GetCustomerBasicInfo(Guid id);
    Task<List<CustomerBasicDto>> GetCustomerPagination(string searchText, int pageNo, int size);

    Task<OperationResult> AddCustomerAddress(Guid customerId, List<CustomerAddressDto> addresses);
    Task<OperationResult> UpdateCustomerAddress(List<CustomerAddressDto> addresses);
    Task<OperationResult> DeleteCustomerAddress(Guid addressId);
    Task<List<CustomerAddressDto>> GetCustomersAddresses(Guid customerId);

    Task<OperationResult> AddPaymentMethod(Guid customerId, List<PaymentMethodDto> paymentMethod);
    Task<OperationResult> UpdatePaymentMethod(List<PaymentMethodDto> paymentMethod);
    Task<OperationResult> DeletePaymentMethod(Guid Id);
    Task<List<PaymentMethodDto>> GetCustomersPaymentMethods(Guid customerId);

    Task<List<CustomerBasicDto>> GetCustomersOfSpecificAddress(string address);
}
