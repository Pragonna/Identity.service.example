using System.Windows.Input;
using Shared.DataAccess.Commands;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Command;

public record RemoveCryptoWalletCommand(string userId, CryptoWalletDto cryptoWalletDto) : ICommand<UserPortfolioDto>;