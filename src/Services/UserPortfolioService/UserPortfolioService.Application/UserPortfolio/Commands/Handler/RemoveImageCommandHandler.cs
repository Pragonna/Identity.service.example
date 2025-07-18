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

public class RemoveImageCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    IUserPortfolioRepository repository) : CommandHandler<RemoveImageCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(RemoveImageCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await repository.GetUserPortfolioWithIncludesByUserId(request.userId);
        userPortfolioEntity.EnsureNotNull();
        var userPortfolio = mapper.Map<Domain.UserPortfolioAggregate.UserPortfolio>(userPortfolioEntity);
        userPortfolio.RemoveImage();
        userPortfolioEntity = mapper.Map<UserPortfolioEntity>(userPortfolio);
        await repository.ModifyEntityAsync(userPortfolioEntity);
        var userPortFolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortFolioDto);
    }
}