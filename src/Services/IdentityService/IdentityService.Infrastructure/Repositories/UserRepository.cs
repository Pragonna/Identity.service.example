using IdentityService.Application.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Entities.Common;
using IdentityService.Domain.Enums;
using IdentityService.Infrastructure.Context;
using IdentityService.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Repositories;

namespace IdentityService.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, UserDbContext>, IUserRepository
{
    private readonly UserDbContext _dbContext;

    public UserRepository(UserDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> FindUserByEmailAsync(string email,
        Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null,
        bool enableTracking = false)
    {
        IQueryable<User> query = _dbContext.Set<User>();
        if (include != null) query = include(query);
        if (!enableTracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<UserOperationClaim> AddUserOperationClaimWhenInsertUser(User user,
        OperationClaim operationClaim = null)
    {
        if (operationClaim == null)
            operationClaim = _dbContext.OperationClaims.FirstOrDefault(o => o.Role == Role.User);
        var userOperationClaim = new UserOperationClaim(user.Id, operationClaim.Id);
        _dbContext.Entry(userOperationClaim).State = EntityState.Added;
        return userOperationClaim;
    }

    public async Task AddOperationClaim(OperationClaim operationClaim)
    {
        await _dbContext.OperationClaims.AddAsync(operationClaim);
    }

    public List<OperationClaim> GetOperationClaimsByUser(Guid userId)
    {
        var operationClaims = _dbContext.UserOperationClaims.Where(e => e.UserId == userId)
            .Select(e => e.OperationClaim).ToList();
        return operationClaims;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        var token = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == refreshToken.UserId);
        if (token is null) _dbContext.Entry(refreshToken).State = EntityState.Added;
        else
        {
            refreshToken = await UpdateRefreshToken(refreshToken.UserId);
        }

        return refreshToken;
    }

    public async Task<bool> RemoveRefreshToken(string refreshToken)
    {
        _dbContext.Entry(refreshToken).State = EntityState.Deleted;
        return true;
    }

    public async Task<RefreshToken> GetRefreshToken(string token)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        return refreshToken;
    }

    public async Task<RefreshToken> GetRefreshToken(Guid userId)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
        return refreshToken;
    }

    public async Task<RefreshToken> UpdateRefreshToken(Guid userId, bool isRevoked = false)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = isRevoked;
            refreshToken.OldRefreshToken = refreshToken.Token;
            refreshToken.Token = RefreshTokenCreator.GenerateRefreshToken;
        }

        _dbContext.Entry(refreshToken).State = EntityState.Modified;
        return refreshToken;
    }

    public async Task<OperationClaim> GetOperationClaimByRole(Role role)
    {
        var operationClaim = await _dbContext.Set<OperationClaim>().FirstOrDefaultAsync(o => o.Role == role);
        return operationClaim;
    }
}