namespace MarketHub.ProductModule.Contracts;


public record CreateItemCommand(Guid Id,string Name);
public record UpdateItemCommand(Guid Id,string Name);
public record DeleteItemCommand(Guid Id);