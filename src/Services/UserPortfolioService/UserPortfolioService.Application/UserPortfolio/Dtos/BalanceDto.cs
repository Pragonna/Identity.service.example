namespace UserPortfolioService.UserPortfolio.Dtos;

public class BalanceDto
{
    public decimal CryptoAmount { get; set; }
    public string Currency { get; set; }
    public decimal CurrencyPrice { get; set; }
}