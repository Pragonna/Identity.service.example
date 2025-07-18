using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.Enums;

namespace UserPortfolioService.Domain.UserPortfolioAggregate;

public sealed class CryptoWallet
{
    public CryptoWallet()
    {
    }

    private CryptoWallet(string walletAddress, string message, string signature, WalletType walletType) : this()
    {
        WalletAddress = walletAddress;
        Message = message;
        Signature = signature;
        WalletType = walletType;
    }

    public string WalletAddress { get; private set; }
    public string Message { get; private set; }
    public ICollection<Balance> Balances { get; private set; } = [];
    public decimal TotalUsdBalance => Balances.Select(b => b.UsdAmount).Sum();
    public string Signature { get; private set; }
    public WalletType WalletType { get; private set; }

    public static CryptoWallet Create(string walletAddress, string message, string signature, WalletType walletType)
    {
        return new CryptoWallet(walletAddress, message, signature, walletType);
    }

    public Balance AddBalance(Balance balance)
    {
        Balances.Add(balance);
        return balance;
    }

    public void RemoveBalance(Balance balance)
    {
        Balances.Remove(balance);
    }
}