using UserPortfolioService.Domain.Enums;

namespace UserPortfolioService.UserPortfolio.Dtos;

public class CryptoWalletDto
{
    public string WalletAddress { get; set; }
    public string Message { get; set; }
    public string Signature { get; set; }
    public List<BalanceDto>? BalanceDtos { get; set; }
    public WalletType WalletType { get; set; }
}