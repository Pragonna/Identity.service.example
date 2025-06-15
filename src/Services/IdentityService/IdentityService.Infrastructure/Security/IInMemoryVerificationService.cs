using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Dtos;

namespace IdentityService.Infrastructure.Security;

public interface IInMemoryVerificationService
{
    string GenerateAndStoreOtp(User user);
    string GenerateAndStoreVerificationLink(User user, string resetToken);
    User? ValidateResetToken(string email, string resetToken);
    User ValidateOtp(string userEmail, string otpCode);
}