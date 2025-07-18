using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record AddCryptoWalletCommand(string userId, CryptoWalletDto cryptoWalletDto) : ICommand<UserPortfolioDto>;