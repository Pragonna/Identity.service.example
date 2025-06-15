using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Helpers;

public class OtpGenerator
{
    public static string GenerateOTP => new Random().Next(100000, 999999).ToString();
}