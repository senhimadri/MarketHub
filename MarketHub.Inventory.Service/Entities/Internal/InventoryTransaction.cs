namespace MarketHub.Inventory.Service.Entities.Internal;

public class InventoryTransaction : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

}
