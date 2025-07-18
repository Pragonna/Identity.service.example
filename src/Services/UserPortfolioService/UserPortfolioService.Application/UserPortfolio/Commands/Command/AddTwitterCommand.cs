using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record AddTwitterCommand(string userId, TwitterDto twitterDto) : ICommand<UserPortfolioDto>;