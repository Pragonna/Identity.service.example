using System.Runtime.InteropServices.JavaScript;
using Shared.DataAccess.Common;

namespace UserPortfolioService.Domain.Entities;

public class TwitterEntity : Entity
{
    public TwitterEntity()
    {
    }
    public TwitterEntity(string twitterHandle, string? twitterUserId, string? profileImageUrl, string? displayName, bool isVerified, bool isConnected)
    {
        TwitterHandle = twitterHandle;
        TwitterUserId = twitterUserId;
        ProfileImageUrl = profileImageUrl;
        DisplayName = displayName;
        IsVerified = isVerified;
        IsConnected = isConnected;
    }

    public string TwitterHandle { get; set; }
    public string? TwitterUserId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? DisplayName { get; set; }
    public bool IsVerified { get; set; }
    public bool IsConnected { get; set; }
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    public Guid UserPortfolioId { get; set; }
    public UserPortfolioEntity UserPortfolioEntity { get; set; }
    
}
