namespace MarketHub.Common.Library.Settings;

public class RabbitMQSettings
{
    public string Host { get; init; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}