namespace CryptoTransactionService.Library.Models;

public abstract record Events(Guid Id)
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

public record OpenCryptoAccount(Guid id, decimal Balance, string Currency) : Events(id);

public record CryptoTransfer(Guid id, decimal amount, string currency, string toAddress) : Events(id);

public record CloseCryptoAccount(Guid id, decimal Balance, string Currency) : Events(id);