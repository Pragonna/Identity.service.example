using AutoMapper;
using Shared.DataAccess;
using Shared.DataAccess.Commands;
using Shared.DataAccess.Common;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;
using UserPortfolioService.UserPortfolio.Rules;

namespace UserPortfolioService.UserPortfolio.Commands.Handler;

public class AddCryptoWalletCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    IUserPortfolioRepository repository,
    UserPortfolioBusinessRules rules) : CommandHandler<AddCryptoWalletCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(AddCryptoWalletCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await rules.CryptoWalletCannotDuplicatedWhenInsertOrUpdated(
            request.userId,
            request.cryptoWalletDto.WalletAddress);

        var cryptoWalletEntity = mapper.Map<CryptoWalletEntity>(request.cryptoWalletDto);
        userPortfolioEntity.CryptoWallets.Add(cryptoWalletEntity);
        userPortfolioEntity.CryptoWallets.First().UserPortfolioId = userPortfolioEntity.Id;
        userPortfolioEntity = await repository.AddCryptoWalletUserPortfolio(userPortfolioEntity);

        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);
        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}