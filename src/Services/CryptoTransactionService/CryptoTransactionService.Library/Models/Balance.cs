using Nethereum.Ledger;
using Nethereum.Signer.Ledger;

namespace CryptoTransactionService.Library.Models;

public class Balance
{
    public decimal CryptoAmount { get; set; }
    public string Currency { get; set; }

    public Balance(decimal cryptoAmount, string currency)
    {
        CryptoAmount = cryptoAmount;
        Currency = currency;
    }
}

public class CryptoWallet
{
    public string WalletAddress { get; set; }
    public LedgerExternalSigner ExternalSigner { get; set; }
    public WalletType WalletType { get; set; }
}

public enum WalletType
{
    Metamask = 0
}