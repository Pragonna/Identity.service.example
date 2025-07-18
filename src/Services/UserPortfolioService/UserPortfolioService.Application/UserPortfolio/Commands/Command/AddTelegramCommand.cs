using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record AddTelegramCommand(string userId,TelegramDto telegramDto) : ICommand<UserPortfolioDto>;