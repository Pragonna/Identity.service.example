namespace IdentityService.Application.Exceptions;

public class InvalidResetTokenException : Exception
{
    public InvalidResetTokenException() : base("Reset token is incorrect")
    {
    }
}