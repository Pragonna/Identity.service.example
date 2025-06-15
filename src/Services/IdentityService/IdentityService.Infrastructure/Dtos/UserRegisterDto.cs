using IdentityService.Domain.Enums;

namespace IdentityService.Infrastructure.Dtos;

public class UserRegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}