using IdentityService.Application.Repositories;
using IdentityService.Infrastructure.Context;
using Shared.DataAccess.Repositories;

namespace IdentityService.Infrastructure.Repositories;

public class UserUnitOfWork(UserDbContext context) : UnitOfWork<UserDbContext>(context), IUserUnitOfWork
{
}