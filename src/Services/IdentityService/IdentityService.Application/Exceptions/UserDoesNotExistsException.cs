using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityService.Application.Exceptions;

public class UserDoesNotExistsException : Exception
{
    public UserDoesNotExistsException() : base("Email or password is incorrect")
    {
    }
}