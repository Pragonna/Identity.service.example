namespace IdentityService.Application.Exceptions;

public class SessionExpiredException : Exception
{
    public SessionExpiredException():base("Session has expired")
    {
    }
}