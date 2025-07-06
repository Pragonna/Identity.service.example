using EventBus.EventBus.Base.Abstraction;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Dtos;
using IdentityService.Infrastructure.Events;
using IdentityService.Infrastructure.Helpers;
using IdentityService.Infrastructure.Security;

namespace IdentityService.Infrastructure.Manager;

public class UserAuthManager(
    IUserRepository userRepository,
    ITokenService tokenService,
    IInMemoryVerificationService inMemoryVerificationService,
    IUserUnitOfWork uOw,
    IEventBus eventBus) : IUserAuthManager
{
    public async Task MarkResetTokenAsValidatedAsync(string email, string token)
    {
        await inMemoryVerificationService.MarkResetTokenAsValidatedAsync(email, token);
    }

    public async Task<bool> IsResetTokenValidatedAsync(string email, string token)
    {
        return await inMemoryVerificationService.IsResetTokenValidatedAsync(email, token);
    }

    public async Task<TokenResponseDto> RegisterAsync(UserRegisterDto userRegisterDto, string ipAddress)
    {
        var users = await userRepository.GetAsync(u => u.Email == userRegisterDto.Email);
        if (users.Any()) throw new UserAlreadyExistsException();
        HashingHelper.GenerateHashPassword(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = await userRepository.AddEntityAsync(new User(
            userRegisterDto.Email, userRegisterDto.Username, userRegisterDto.FirstName, userRegisterDto.LastName,
            userRegisterDto.DateOfBirth, userRegisterDto.Gender, passwordSalt, passwordHash));
        var userOperationClaim = await userRepository.AddUserOperationClaimWhenInsertUser(user);
        user.UserOperationClaim.Add(userOperationClaim);

        (AccessToken accessToken, RefreshToken refreshToken) tokens =
            await tokenService.GenerateNewTokensAsync(user, userRepository.GetOperationClaimsByUser(user.Id));

        tokens.refreshToken.CreatedByIp = ipAddress;
        await userRepository.AddRefreshToken(tokens.refreshToken);
        await uOw.CommitAsync();

        return new TokenResponseDto()
        {
            AccessToken = tokens.accessToken.Token,
            RefreshToken = tokens.refreshToken.Token,
            ExpireAt = tokens.accessToken.ExpiresAt
        };
    }

    public async Task LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
        if (user == null) throw new UserDoesNotExistsException();
        var passwordIsCorrect =
            HashingHelper.VerifyHashPassword(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);
        if (!passwordIsCorrect) throw new UserDoesNotExistsException();
        var otp = inMemoryVerificationService.GenerateAndStoreOtp(user);
        await eventBus.Publish(
            new SendOtpVerificationEmailIntegrationEvent(user.Email,
                otp));
    }

    public async Task<TokenResponseDto> ValidateOtp(string email, string otp, string ipAddress)
    {
        var user = inMemoryVerificationService.ValidateOtp(email, otp);
        if (user == null) throw new ArgumentNullException("Retry login");

        var tokens = await tokenService.GenerateNewTokensAsync(user, userRepository.GetOperationClaimsByUser(user.Id));

        user.IsActive = true;
        await userRepository.ModifyEntityAsync(user);
        tokens.refreshToken.CreatedByIp = ipAddress;
        var refreshToken = await userRepository.AddRefreshToken(tokens.refreshToken);

        await uOw.CommitAsync();

        return new TokenResponseDto()
        {
            AccessToken = tokens.accessToken.Token,
            RefreshToken = refreshToken.Token,
            ExpireAt = tokens.accessToken.ExpiresAt
        };
    }

    public async Task LogoutAsync(string email)
    {
        var user = await userRepository.FindUserByEmailAsync(email);
        var refreshToken = await userRepository.GetRefreshToken(user.Id);
        var isDeleted = await userRepository.RemoveRefreshToken(refreshToken.Token);
        if (!isDeleted) throw new ArgumentException("Refresh token not found");

        user.IsActive = false;
        await userRepository.ModifyEntityAsync(user);
        await uOw.CommitAsync();
    }

    public async Task<TokenResponseDto> RefreshTokenAsync(string token, string email)
    {
        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == email);
        var refreshToken = await userRepository.GetRefreshToken(token);
        if (refreshToken.IsRevoked && refreshToken.ExpireAt > DateTime.UtcNow) throw new SessionExpiredException();

        var tokens = await tokenService.GenerateNewTokensAsync(user, userRepository.GetOperationClaimsByUser(user.Id));

        refreshToken = await userRepository.UpdateRefreshToken(refreshToken.UserId);
        return new TokenResponseDto()
        {
            AccessToken = tokens.accessToken.Token,
            RefreshToken = refreshToken.Token,
            ExpireAt = tokens.accessToken.ExpiresAt
        };
    }

    public async Task ForgotPasswordAttemptAsync(string email)
    {
        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new UserDoesNotExistsException();

        var resetToken = RefreshTokenCreator.GenerateRefreshToken;

        var verificationLink = inMemoryVerificationService.GenerateAndStoreVerificationLink(user, resetToken);
        await eventBus.Publish(new SendVerificationLinkIntegrationEvent(user.Email, verificationLink));
    }

    public async Task<string> ValidateVerificationLinkAsync(string id, string resetToken)
    {
        var _user = await userRepository.GetByIdAsync(Guid.Parse(id));
        var user = inMemoryVerificationService.ValidateResetToken(_user.Email, resetToken);
        if (user == null) throw new InvalidResetTokenException();
        return user.Email;
    }

    public async Task ResetPasswordAsync(string email, string newPassword)
    {
        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == email);
        HashingHelper.GenerateHashPassword(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await userRepository.ModifyEntityAsync(user);
        await uOw.CommitAsync();
    }
}