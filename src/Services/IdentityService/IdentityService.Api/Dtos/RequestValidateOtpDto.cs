namespace IdentityService.Api.Dtos;

public class RequestValidateOtpDto
{
    public string Email { get; set; }
    public string Otp { get; set; }
}