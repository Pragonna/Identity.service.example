namespace IdentityService.Api.Dtos;

public class RequestResetPasswordDto
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}