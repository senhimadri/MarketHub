﻿namespace MarketHub.ProductModule.Api.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
