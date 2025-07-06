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
        var tokenResponseDto = await userAuthManager.RegisterAsync(userRegisterDto, GetIpAddress());
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
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        };
        Response.Cookies.Append("email", request.Email, cookieOptions);
        Response.Cookies.Append("refreshToken", tokenResponseDto.RefreshToken, cookieOptions);
        
        return Ok(tokenResponseDto);
    }

    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var emailCookie = Request.Cookies["email"];
        if (string.IsNullOrEmpty(refreshToken)&&string.IsNullOrEmpty(emailCookie)) return BadRequest("refresh token or email cookie is missing");
        
        var tokenResponseDto = await userAuthManager.RefreshTokenAsync(refreshToken, emailCookie);
        Response.Cookies.Delete("refreshToken");
        Response.Cookies.Append("refreshToken", tokenResponseDto.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
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
    public async Task<IActionResult> ValidateResetToken(string email, string resetToken)
    {
        var userEmail = await userAuthManager.ValidateVerificationLinkAsync(email, resetToken);
        await userAuthManager.MarkResetTokenAsValidatedAsync(email, resetToken);
        return Ok(new
        {
            email,
            resetToken
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] RequestResetPasswordDto request)
    {
        var isSuccess = await userAuthManager.IsResetTokenValidatedAsync(request.Email, request.ResetToken);
        if (!isSuccess) return BadRequest("validation is invalid");
        await userAuthManager.ResetPasswordAsync(request.Email, request.NewPassword);
        return Ok("Password has been changed");
    }
}