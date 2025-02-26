using MarketHub.CustomerService.Entities;

namespace MarketHub.CustomerService.DataTransferObjects;

public record CustomerBasicDto(Guid Id,string FirstName, string LastName, string? Email, string? PhoneNumber);

public record CustomerDetailsDto(Guid Id, string FirstName, string LastName, string? Email, string? PhoneNumber, 
                    List<CustomerAddressDto>? Addresses) : CustomerBasicDto( Id,  FirstName,  LastName,   Email,   PhoneNumber);
public record CreateCustomerBasicInfoDto(string FirstName, string LastName, string? Email, string? PhoneNumber, 
                                        List<CustomerAddressDto>? Addresses);
public record UpdateCustomerBasicInfoDto(string FirstName, string LastName, string? Email, string? PhoneNumber);
public record CustomerAddressDto(Guid Id, string? Street, string? City ,string? State, string? ZipCode);
public record PaymentMethodDto(Guid Id,PaymentType Type, bool IsDefault, 
                string? CardNumber,string? CardHolderName,DateTime? ExpiryDate,
                string? PayPalEmail,
                string? BankAccountName,string? BankAccountNumber,
                string? MobileWalletProvider,string? MobileWalletNumber
            );

