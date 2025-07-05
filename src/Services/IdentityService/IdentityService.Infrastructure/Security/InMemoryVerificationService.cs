using EventBus.EventBus.Base.Abstraction;
using IdentityService.Application.Exceptions;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Events;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityService.Infrastructure.Security;

public class InMemoryVerificationService(IMemoryCache memoryCache) : IInMemoryVerificationService
{
    private static readonly Dictionary<string, (string Code, DateTime ExpireAt, User User)> _otpStore = new();

    public string GenerateAndStoreOtp(User user)
    {
        var otpCode = new Random().Next(100000, 999999).ToString();
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        memoryCache.Set(user.Email + "_otp", (otpCode, user), cacheEntryOptions);
        return otpCode;
    }

    public string GenerateAndStoreVerificationLink(User user, string resetToken)
    {
        var encodedToken = Uri.EscapeDataString(resetToken);
        var verificationLink =
            $"https://localhost:7022/api/auth/validate-resetToken?email={user.Email}&resetToken={encodedToken}";
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        memoryCache.Set(user.Email + "_resetToken", (resetToken, user), cacheEntryOptions);
        return verificationLink;
    }

    public User? ValidateResetToken(string email, string resetToken)
    {
        var decodedToken = Uri.UnescapeDataString(resetToken);
        if (memoryCache.TryGetValue<(string _resetToken, User user)>(email + "_resetToken", out var entry)) ;
        var (_resetToken, user) = entry;

        if (_resetToken != decodedToken) throw new InvalidResetTokenException();
        memoryCache.Remove(user.Email + "_resetToken");

        return user;
    }

    public User ValidateOtp(string email, string otpCode)
    {
        if (!memoryCache.TryGetValue<(string Code, User User)>(email + "_otp", out var entry))
            throw new OtpCodeIncorrectException();

        var (code, user) = entry;

        if (code != otpCode)
            throw new OtpCodeIncorrectException();

        memoryCache.Remove(email);
        return user;
    }
    public Task MarkResetTokenAsValidatedAsync(string email, string token)
    {
        var cacheKey = GetResetTokenCacheKey(email, token);
        memoryCache.Set(cacheKey, true, TimeSpan.FromMinutes(15));
        return Task.CompletedTask;
    }

    public Task<bool> IsResetTokenValidatedAsync(string email, string token)
    {
        var cacheKey = GetResetTokenCacheKey(email, token);
        return Task.FromResult(memoryCache.TryGetValue(cacheKey, out _));
    }

    private string GetResetTokenCacheKey(string email, string token) =>
        $"reset_token_validated:{email}:{token}";
}