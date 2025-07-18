namespace UserPortfolioService.Domain.UserPortfolioAggregate;

public sealed class Twitter
{
    public Twitter()
    {
    }

    private Twitter(string twitterHandle, string? twitterUserId, string? profileImageUrl, string? displayName,
        bool isVerified, bool isConnected) : this()
    {
        TwitterHandle = twitterHandle;
        TwitterUserId = twitterUserId;
        ProfileImageUrl = profileImageUrl;
        DisplayName = displayName;
        IsVerified = isVerified;
        IsConnected = isConnected;
    }

    public string TwitterHandle { get; private set; }
    public string? TwitterUserId { get; private set; }
    public string? ProfileImageUrl { get; set; }
    public string? DisplayName { get; private set; }
    public bool IsVerified { get; private set; }
    public bool IsConnected { get; private set; }
    public DateTime ConnectedAt { get; private set; } = DateTime.UtcNow;

    public static Twitter Create(string twitterHandle, string? twitterUserId, string? profileImageUrl,
        string? displayName, bool isVerified, bool isConnected)
    {
        return new Twitter(twitterHandle, twitterUserId, profileImageUrl, displayName, isVerified, isConnected);
    }
}