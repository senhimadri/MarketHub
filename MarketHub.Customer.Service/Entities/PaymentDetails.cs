namespace MarketHub.CustomerModule.Api.Entities;

public class PaymentDetails
{
    public string? CardNumber { get; set; } 
    public string? CardHolderName { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public string? PayPalEmail { get; set; }

    public string? BankAccountName { get; set; }
    public string? BankAccountNumber { get; set; }

    public string? MobileWalletProvider { get; set; }
    public string? MobileWalletNumber { get; set; }
}
