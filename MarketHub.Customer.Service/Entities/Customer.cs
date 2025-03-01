using MarketHub.CustomerModule.Api.Entities;

namespace MarketHub.CustomerModule.Api.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<PaymentMethod>? PaymentMethods { get; set; } 
}
