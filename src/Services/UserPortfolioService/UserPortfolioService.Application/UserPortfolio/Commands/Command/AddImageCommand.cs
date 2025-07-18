using Shared.DataAccess.Commands;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record AddImageCommand(string userId, ImageType image) : ICommand<UserPortfolioDto>;