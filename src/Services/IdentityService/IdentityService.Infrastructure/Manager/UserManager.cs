using IdentityService.Application.Exceptions;
using IdentityService.Application.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess;

namespace IdentityService.Infrastructure.Manager;

public class UserManager(IUserRepository userRepository, IUserUnitOfWork userUnitOfWork) : IUserManager
{
    public async Task<Paginate<User>> GetAllUsers()
    {
        var result = await userRepository.GetPaginateAsync(
            include: i => i.Include(u => u.UserOperationClaim).ThenInclude(i => i.OperationClaim));
        return result;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var result = await userRepository.FindUserByEmailAsync(email);
        if (result == null) throw new UserDoesNotExistsException();
        return result;
    }

    public async Task<User> ChangeUserRole(string email, Role role)
    {
        var user = await GetUserByEmail(email);
        var operationClaim = await userRepository.GetOperationClaimByRole(role);

        if (user == null) throw new UserDoesNotExistsException();
        if (operationClaim == null) throw new ArgumentNullException("This role does not exist");

        var userOperationClaim = await userRepository.AddUserOperationClaimWhenInsertUser(user, operationClaim);

        user.UserOperationClaim.Add(userOperationClaim);
        await userUnitOfWork.CommitAsync();
        return user;
    }

    public async Task<User> DeleteUser(string email)
    {
        var user = await GetUserByEmail(email);
        if (user == null) throw new UserNotFoundException();
        var deletedUser = await userRepository.DeleteEntityAsync(user);
        await userUnitOfWork.CommitAsync();
        return deletedUser;
    }
}