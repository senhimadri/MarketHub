namespace MarketHub.Customer.Service;

public class MongoDbSetting
{
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }
    public string ConnectionString => $"mongodb://{Host}:{Port}";
}
