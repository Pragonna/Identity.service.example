namespace IdentityService.Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("Email already exists")
    {
    }
}