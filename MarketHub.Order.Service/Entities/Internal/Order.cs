using MarketHub.OrderModule.Api.Entities.External;

namespace MarketHub.OrderModule.Api.Entities.Internal;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public List<OrderItem> Items { get; set; } = new();
}
