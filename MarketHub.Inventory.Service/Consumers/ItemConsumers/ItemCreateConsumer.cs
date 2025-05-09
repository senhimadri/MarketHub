using MarketHub.InventoryModule.Api.ExternalEntities;
using MarketHub.ProductModule.Contracts;
using MassTransit;

namespace MarketHub.OrderModule.Api.Consumers.ItemConsumers;

public class ItemCreateConsumer : IConsumer<CreateItemCommand>
{
    public async Task Consume(ConsumeContext<CreateItemCommand> context)
    {
        var message = context.Message;

        Item item = new Item
        {
            Id = message.Id,
            Name = message.Name
        };

        Console.WriteLine($"Write From Inventory Module ,Item Id:{item.Id} , Name :{item.Name}");

        await Task.CompletedTask;
    }
}
