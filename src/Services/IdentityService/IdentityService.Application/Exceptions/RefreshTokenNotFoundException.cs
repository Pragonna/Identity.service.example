namespace IdentityService.Application.Exceptions;

public class RefreshTokenNotFoundException : Exception
{
    public RefreshTokenNotFoundException():base("Refresh token not found")
    {
    }
}