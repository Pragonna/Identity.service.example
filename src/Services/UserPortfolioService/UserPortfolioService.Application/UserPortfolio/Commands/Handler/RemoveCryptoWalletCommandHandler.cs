using AutoMapper;
using Shared.DataAccess;
using Shared.DataAccess.Commands;
using Shared.DataAccess.Common;
using Shared.DataAccess.Repositories;
using UserPortfolioService.Application.Extensions;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Handler;

public class RemoveCryptoWalletCommandHandler(
    IUnitOfWork unitOfWork,
    IUserPortfolioRepository repository,
    IMapper mapper) : CommandHandler<RemoveCryptoWalletCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(RemoveCryptoWalletCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await repository.GetUserPortfolioWithIncludesByUserId(request.userId);
        userPortfolioEntity.EnsureNotNull();
        var cryptoWallet = mapper.Map<CryptoWalletEntity>(request.cryptoWalletDto);
        userPortfolioEntity.CryptoWallets.Remove(cryptoWallet);
        await repository.RemoveCryptoWalletUserPortfolio(userPortfolioEntity);
        var responseDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);
        return Result<UserPortfolioDto, Error>.Success(responseDto);
    }
}