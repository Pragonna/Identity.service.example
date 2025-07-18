using AutoMapper;
using Shared.DataAccess;
using Shared.DataAccess.Commands;
using Shared.DataAccess.Common;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.UserPortfolioAggregate;
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
        var cryptoWallet = CryptoWallet.Create(request.cryptoWalletDto.WalletAddress, request.cryptoWalletDto.Message,
            request.cryptoWalletDto.Signature, request.cryptoWalletDto.WalletType);
        var userPortfolioEntity = await rules.CryptoWalletCannotDuplicatedWhenInsertOrUpdated(
            request.userId,
            cryptoWallet.WalletAddress);

        var userPortfolio = mapper.Map<Domain.UserPortfolioAggregate.UserPortfolio>(userPortfolioEntity);
        userPortfolio.AddCryptoWallet(cryptoWallet);
        userPortfolioEntity = mapper.Map<UserPortfolioEntity>(userPortfolio);
        await repository.ModifyEntityAsync(userPortfolioEntity);

        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);
        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}