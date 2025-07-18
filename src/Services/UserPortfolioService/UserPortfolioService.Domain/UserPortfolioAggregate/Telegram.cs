namespace UserPortfolioService.Domain.UserPortfolioAggregate;

public sealed class Telegram
{
    public Telegram()
    {
    }

    private Telegram(string telegramHandle, long telegramUserId, string? fullName, bool isSubscribed) : this()
    {
        TelegramHandle = telegramHandle;
        TelegramUserId = telegramUserId;
        FullName = fullName;
        IsSubscribed = isSubscribed;
    }

    public string TelegramHandle { get; private set; }
    public long TelegramUserId { get; private set; }
    public string? FullName { get; private set; }
    public bool IsSubscribed { get; private set; }
    public DateTime ConnectedAt { get; private set; } = DateTime.UtcNow;

    public static Telegram Create(string telegramHandle, long telegramUserId, string? fullName, bool isSubscribed)
    {
        return new Telegram(telegramHandle, telegramUserId, fullName, isSubscribed);
    }
}