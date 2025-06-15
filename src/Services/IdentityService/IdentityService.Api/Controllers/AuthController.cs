using IdentityService.Api.Dtos;
using IdentityService.Infrastructure.Dtos;
using IdentityService.Infrastructure.Manager;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[Route("api/[controller]")]
public class AuthController(
    ILogger<AuthController> logger,
    IUserAuthManager userAuthManager) : ControllerBase
{
    private string? GetIpAddress()
    {
        // Check if the request contains the "X-Forwarded-For" header.
        // This header is used when the request goes through a proxy or load balancer.
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];

        // If "X-Forwarded-For" is not found, get the remote IP address of the client.
        // Convert IPv6 to IPv4 if needed and return it as a string.
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var tokenResponseDto = await userAuthManager.RegisterAsync(userRegisterDto,GetIpAddress());
        return Ok(tokenResponseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        await userAuthManager.LoginAsync(userLoginDto);
        return Ok("Otp code has been successfully sent.");
    }

    [HttpPost("validate-otp")]
    public async Task<IActionResult> ValidateOtp(
        [FromBody] RequestValidateOtpDto request)
    {
        var tokenResponseDto = await userAuthManager.ValidateOtp(request.Email, request.Otp, GetIpAddress());
        return Ok(tokenResponseDto);
    }

    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken(string email)
    {
        var tokenResponseDto = await userAuthManager.RefreshTokenAsync(email);
        return Ok(tokenResponseDto);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout(string email)
    {
        await userAuthManager.LogoutAsync(email);
        return Ok("Logged out");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        await userAuthManager.ForgotPasswordAttemptAsync(email);
        return Ok("Reset password link has been sent.");
    }

    [HttpGet("validate-resetToken")]
    public async Task<IActionResult> ValidateResetToken(string id, string resetToken)
    {
        var userEmail = await userAuthManager.ValidateVerificationLinkAsync(id, resetToken);
        return Ok(userEmail);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] RequestResetPasswordDto request)
    {
        await userAuthManager.ResetPasswordAsync(request.Email, request.NewPassword);
        return Ok("Password has been changed");
    }
}