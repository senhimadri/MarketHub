namespace MarketHub.CustomerService.Settings;

public class MongoDbSetting
{
    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 27017;
    public string Username { get; init; } = "root";
    public string Password { get; init; } = "examplepassword";
    public string ConnectionString => $"mongodb://{Username}:{Password}@{Host}:{Port}/?authSource=admin";

}
