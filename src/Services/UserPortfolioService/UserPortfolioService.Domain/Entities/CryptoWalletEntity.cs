using Shared.DataAccess.Common;
using UserPortfolioService.Domain.Enums;

namespace UserPortfolioService.Domain.Entities;

public class CryptoWalletEntity : Entity
{
    public CryptoWalletEntity()
    {
        Balances = new HashSet<Balance>();
    }
    public CryptoWalletEntity(string walletAddress, string message, string signature, WalletType walletType):this()
    {
        WalletAddress = walletAddress;
        Message = message;
        Signature = signature;
        WalletType = walletType;
    }

    public string WalletAddress { get; set; }
    public string Message { get; set; }
    public string Signature { get; set; }
    public ICollection<Balance>? Balances { get; set; }
    public WalletType WalletType { get; set; }
    public Guid UserPortfolioId { get; set; }
    public UserPortfolioEntity UserPortfolioEntity { get; set; }
}
// {
// "walletAddress": "0x123...",
// "signature": "...",
// "message": "...",
// "walletType": "Metamask",
// "balances": [
// { "type": "ETH", "amount": 0.1 },
// { "type": "USDT", "amount": 25.45 },
// { "type": "DAI", "amount": 100.00 }
// ]
// }


//using Nethereum.Signer;
// 
// public bool VerifySignature(string walletAddress, string message, string signature)
// {
//     var signer = new EthereumMessageSigner();
//     var recoveredAddress = signer.EncodeUTF8AndEcRecover(message, signature);
//     return recoveredAddress.Equals(walletAddress, StringComparison.OrdinalIgnoreCase);
// }