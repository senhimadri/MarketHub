namespace MarketHub.CustomerModule.Api.Entities;

public  class PaymentMethod : BaseEntity
{
    public PaymentType Type { get; set; }
    public PaymentDetails Details { get; set; } = new();
    public bool IsDefault { get; set; } = false;
}
