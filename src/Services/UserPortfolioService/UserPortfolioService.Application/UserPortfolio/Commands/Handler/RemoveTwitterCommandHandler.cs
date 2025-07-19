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

public class RemoveTwitterCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    IUserPortfolioRepository repository) : CommandHandler<RemoveTwitterCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(RemoveTwitterCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await repository.GetUserPortfolioWithIncludesByUserId(request.userId);
        userPortfolioEntity.EnsureNotNull();
        userPortfolioEntity.TwitterEntity = null;
        await repository.RemoveTwitterUserPortfolio(userPortfolioEntity);
        var userPortFolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortFolioDto);
    }
}