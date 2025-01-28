namespace MarketHub.Inventory.Service.Entities.Internal;

public class InventoryTransactionType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int EffectOnStock { get; set; }
}
