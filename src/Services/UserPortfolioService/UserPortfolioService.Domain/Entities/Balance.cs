using Shared.DataAccess.Common;

namespace UserPortfolioService.Domain.Entities;

public class Balance
{
    public decimal CryptoAmount { get; set; }
    public string Currency { get; set; }
    public decimal CurrencyPrice { get; set; }
    public decimal UsdAmount => CryptoAmount * CurrencyPrice;

    public Balance(decimal cryptoAmount, string currency, decimal currencyPrice)
    {
        CryptoAmount = cryptoAmount;
        Currency = currency;
        CurrencyPrice = currencyPrice;
    }
}