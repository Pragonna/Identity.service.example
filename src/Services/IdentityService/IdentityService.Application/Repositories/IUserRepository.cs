using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Repositories;

namespace IdentityService.Application.Repositories;

public interface IUserRepository : IWriteRepository<User>, IReadRespository<User>
{
    Task<User> FindUserByEmailAsync(string email,
        Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null,
        bool enableTracking = false);

    Task<UserOperationClaim> AddUserOperationClaimWhenInsertUser(User user, OperationClaim operationClaim = null);
    List<OperationClaim> GetOperationClaimsByUser(Guid userId);
    Task AddOperationClaim(OperationClaim operationClaim);
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string token);
    Task<RefreshToken> GetRefreshToken(Guid userId);
    Task<RefreshToken> UpdateRefreshToken(Guid userId, bool isRevoked = false);
    Task<OperationClaim> GetOperationClaimByRole(Role role);
}