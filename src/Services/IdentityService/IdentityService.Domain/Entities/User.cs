using IdentityService.Domain.Entities.Common;
using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class User : BaseUserEntity
{
    public User()
    {
        UserOperationClaim = new HashSet<UserOperationClaim>();
    }

    public User(string email, string username, string firstName, string lastName, DateTime dateOfBirth, Gender gender,
        byte[] passwordSalt, byte[] passwordHash, Image? image = null) : this()
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Image = image;
    }

    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public Image? Image { get; set; }
    public bool IsActive { get; set; } = false;
    public RefreshToken RefreshToken { get; set; }
    public ICollection<UserOperationClaim> UserOperationClaim { get; set; }
}