using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Dtos;

namespace IdentityService.Infrastructure.Manager;

public interface IUserAuthManager
{
    Task MarkResetTokenAsValidatedAsync(string email, string token);
    Task<bool> IsResetTokenValidatedAsync(string email, string token);
    Task<TokenResponseDto> RegisterAsync(UserRegisterDto userRegisterDto,string ipAddress);
    Task LoginAsync(UserLoginDto userLoginDto);
    Task<TokenResponseDto> ValidateOtp(string email, string otp,string ipAddress);
    Task<TokenResponseDto> RefreshTokenAsync(string email);
    Task ForgotPasswordAttemptAsync(string email);
    Task<string> ValidateVerificationLinkAsync(string email, string resetToken);
    Task ResetPasswordAsync(string email, string newPassword);
    Task LogoutAsync(string email);
}