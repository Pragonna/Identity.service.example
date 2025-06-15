using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Shared.DataAccess;

namespace IdentityService.Infrastructure.Manager;

public interface IUserManager
{
    Task<Paginate<User>> GetAllUsers();
    Task<User> GetUserByEmail(string email);
    Task<User> ChangeUserRole(string email, Role role);
    Task<User> DeleteUser(string email);
}