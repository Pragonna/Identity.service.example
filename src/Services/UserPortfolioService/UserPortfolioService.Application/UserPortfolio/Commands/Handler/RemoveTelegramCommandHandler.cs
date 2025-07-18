using AutoMapper;
using Shared.DataAccess;
using Shared.DataAccess.Commands;
using Shared.DataAccess.Common;
using UserPortfolioService.Application.Extensions;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Commands.Handler;

public class RemoveTelegramCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    IUserPortfolioRepository repository) : CommandHandler<RemoveTelegramCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(RemoveTelegramCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await repository.GetUserPortfolioWithIncludesByUserId(request.userId);
        userPortfolioEntity.EnsureNotNull();
        var userPortfolio = mapper.Map<Domain.UserPortfolioAggregate.UserPortfolio>(userPortfolioEntity);
        userPortfolio.RemoveTelegram();
        userPortfolioEntity = mapper.Map<UserPortfolioEntity>(userPortfolio);
        await repository.ModifyEntityAsync(userPortfolioEntity);
        var userPortFolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortFolioDto);
    }
}