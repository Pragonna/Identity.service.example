using System.Security.Claims;
using IdentityService.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserPortfolioService.Api.Controllers.Common;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.Api.Controllers;

[Route("api/[controller]")]
public class UserPortfolioManageController(
    IMediator mediator,
    ILogger<UserPortfolioManageController> logger,
    IHttpContextAccessor contextAccessor) : BaseController(mediator)
{
    string userId = contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value?.ToString() ??
                    throw new UserNotFoundException();

    [HttpGet("get-userPortfolio")]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var claims = contextAccessor.HttpContext.User.Claims ?? throw new UserNotFoundException();
        var names = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Split();
        var userInfo = new
        {
            UserId = userId,
            Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value.ToString(),
            FirstName = names[0],
            LastName = names[1],
            Gender = claims.FirstOrDefault(c => c.Type == ClaimTypes.Gender)?.Value.ToString(),
            DateOfBirth = claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value.ToString()
        };

        //if user exists will throw exception
        var result = await _mediator.Send(new AddUserPortfolioCommand(
            userInfo.UserId,
            userInfo.Email,
            userInfo.FirstName,
            userInfo.LastName,
            userInfo.Gender,
            userInfo.DateOfBirth));

        return Ok(result);
    }

    [HttpPost("add-wallet")]
    public async Task<IActionResult> AddWallet([FromBody] CryptoWalletDto cryptoWalletDto)
    {
        var result = await _mediator.Send(new AddCryptoWalletCommand(userId, cryptoWalletDto));
        return Ok(result);
    }

    [HttpPost("add-telegram")]
    public async Task<IActionResult> AddTelegram([FromBody] TelegramDto telegramDto)
    {
        var result = await _mediator.Send(new AddTelegramCommand(userId, telegramDto));
        return Ok(result);
    }

    [HttpPost("add-twitter")]
    public async Task<IActionResult> AddTwitter([FromBody] TwitterDto twitterDto)
    {
        var result = await _mediator.Send(new AddTwitterCommand(userId, twitterDto));
        return Ok(result);
    }

    [HttpPost("add-image")]
    public async Task<IActionResult> AddImage(IFormFile file)
    {
        var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var result =
            await _mediator.Send(new AddImageCommand(userId,
                new ImageType(file.FileName, file.ContentType, ms.ToArray())));
        return Ok(result);
    }

    [HttpDelete("delete-wallet")]
    public async Task<IActionResult> DeleteWallet([FromBody] CryptoWalletDto cryptoWalletDto)
    {
        var result = await _mediator.Send(new RemoveCryptoWalletCommand(userId, cryptoWalletDto));
        return Ok(result);
    }

    [HttpDelete("delete-telegram")]
    public async Task<IActionResult> DeleteTelegram()
    {
        var result = await _mediator.Send(new RemoveTelegramCommand(userId));
        return Ok(result);
    }

    [HttpDelete("delete-twitter")]
    public async Task<IActionResult> DeleteTwitter()
    {
        var result = await _mediator.Send(new RemoveTwitterCommand(userId));
        return Ok(result);
    }

    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImage()
    {
        var result = await _mediator.Send(new RemoveImageCommand(userId));
        return Ok(result);
    }
}