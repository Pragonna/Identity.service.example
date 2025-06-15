using IdentityService.Domain.Enums;
using IdentityService.Infrastructure.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class AdminPanelController(
    ILogger<AdminPanelController> logger,
    IUserManager userManager) : ControllerBase
{
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUser()
    {
        var result = await userManager.GetAllUsers();
        return Ok(result);
    }

    [HttpGet("get-user-by-email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await userManager.GetUserByEmail(email);
        return Ok(result);
    }

    [HttpPut("change-user-role")]
    public async Task<IActionResult> ChangeUserRole(string email, Role role)
    {
        var result = await userManager.ChangeUserRole(email, role);
        return Ok(result);
    }

    [HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var result = await userManager.DeleteUser(email);
        return Ok(result);
    }
}