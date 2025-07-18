using CryptoTransactionService.Library.Models;

namespace CryptoTransactionService.Library;

public class CryptoAccount
{
    public Guid GuidId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public List<Events> Events { get; set; } = [];

    private CryptoAccount()
    {
        GuidId = Guid.NewGuid();
    }

    public static CryptoAccount Create(decimal balance, string currency)
    {
        return new CryptoAccount();
    }

    private void Apply(Events @event)
    {
        if (@event is null) throw new ArgumentNullException("Events cannot be null");

        switch (@event)
        {
            case OpenCryptoAccount openCryptoAccount:
                Balance = openCryptoAccount.Balance;
                Currency = openCryptoAccount.Currency;
                break;
            case CryptoTransfer cryptoTransfer:
                Balance -= cryptoTransfer.amount;
                Currency = cryptoTransfer.currency;
                break;
            case CloseCryptoAccount closeCryptoAccount:
                CloseCryptoAccount(this);
                break;
        }
    }

    public void CloseCryptoAccount(CryptoAccount cryptoAccount)
    {
    }
}