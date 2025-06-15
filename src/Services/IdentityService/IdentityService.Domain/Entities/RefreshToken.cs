using IdentityService.Domain.Entities.Common;
using Shared.DataAccess.Common;

namespace IdentityService.Domain.Entities;

public class RefreshToken : Entity
{
    public RefreshToken()
    {
    }
    public RefreshToken(string token, Guid userId, bool isRevoked,
        DateTime revokedAt = default,
        string createdByIp = null, string oldRefreshToken = null)
    {
        Token = token;
        UserId = userId;
        IsRevoked = isRevoked;
        CreatedAt = DateTime.UtcNow;
        RevokedAt = revokedAt;
        CreatedByIp = createdByIp;
        OldRefreshToken = oldRefreshToken;
        ExpireAt = CreatedAt.AddHours(1);
    }

    public User User { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string CreatedByIp { get; set; }
    public string? OldRefreshToken { get; set; }
}