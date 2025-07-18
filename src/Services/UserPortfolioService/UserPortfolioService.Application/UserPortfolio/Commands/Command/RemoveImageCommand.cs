using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record RemoveImageCommand(string userId) : ICommand<UserPortfolioDto>;