using System.Security.Cryptography;

namespace IdentityService.Infrastructure.Helpers;

public class RefreshTokenCreator
{
    public static string GenerateRefreshToken
    {
        get
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}