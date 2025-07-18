using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record AddUserPortfolioCommand(
    string userId,
    string email,
    string firstName,
    string lastName,
    string gender,
    string dateOfBirth) : ICommand<UserPortfolioDto>;