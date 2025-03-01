﻿namespace MarketHub.InventoryModule.Api.ExternalEntities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}

