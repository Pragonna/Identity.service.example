using System.Reflection.Metadata.Ecma335;
using Shared.DataAccess.Common;

namespace UserPortfolioService.Domain.Entities;

public class TelegramEntity : Entity
{
    public TelegramEntity()
    {
    }
    public TelegramEntity(string telegramHandle, long telegramUserId, string? fullName, bool isSubscribed)
    {
        TelegramHandle = telegramHandle;
        TelegramUserId = telegramUserId;
        FullName = fullName;
        IsSubscribed = isSubscribed;
    }

    public string TelegramHandle { get; set; }
    public long TelegramUserId { get; set; }
    public string? FullName { get; set; }
    public Guid UserPortfolioId { get; set; }
    public bool IsSubscribed { get; set; }
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    public UserPortfolioEntity UserPortfolioEntity { get; set; }
}