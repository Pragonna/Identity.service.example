namespace IdentityService.Infrastructure.Dtos;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
}