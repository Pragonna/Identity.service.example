using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Security;

public class JwtTokenService(IOptions<TokenOptions> options) : ITokenService
{
    private readonly TokenOptions _options = options.Value;
    private DateTime _accessTokenExpiration;

    public async Task<(AccessToken accessToken, RefreshToken refreshToken)> GenerateNewTokensAsync(User user,
        List<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiration);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwt = CreateJwtSecurityToken(_options, user, credentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return (new AccessToken(token, _accessTokenExpiration), new RefreshToken(
            RefreshTokenCreator.GenerateRefreshToken,
            user.Id,
            isRevoked: false));
    }

    public Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        //
        throw new NotImplementedException();
    }

    public Task RevokeTokenAsync(RefreshToken token)
    {
        //
        throw new NotImplementedException();
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
        User user,
        SigningCredentials signingCredentials,
        List<OperationClaim> operationClaims)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration, 
            notBefore: DateTime.UtcNow,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

        operationClaims
            .Select(c => c.Role)
            .ToList()
            .ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.ToString())));

        return claims;
    }
}