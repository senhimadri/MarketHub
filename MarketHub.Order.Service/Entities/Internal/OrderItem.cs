using MarketHub.OrderModule.Api.ExternalEntities;

namespace MarketHub.OrderModule.Api.Entities.Internal;

public class OrderItem : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
