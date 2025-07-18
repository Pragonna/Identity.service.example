using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record RemoveTwitterCommand(string userId) : ICommand<UserPortfolioDto>;