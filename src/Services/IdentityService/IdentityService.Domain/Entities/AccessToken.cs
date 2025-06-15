namespace IdentityService.Domain.Entities;

public class AccessToken
{
    public AccessToken(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}