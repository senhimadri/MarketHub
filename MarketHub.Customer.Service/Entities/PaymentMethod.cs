using MarketHub.Product.Service.Entities;

namespace MarketHub.Customer.Service.Entities;

public class PaymentMethod : BaseEntity
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
