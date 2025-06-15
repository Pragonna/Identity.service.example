using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Security;

public interface ITokenService
{
    Task<(AccessToken accessToken, RefreshToken refreshToken)> GenerateNewTokensAsync(User user,List<OperationClaim> operationClaims);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeTokenAsync(RefreshToken token);
}