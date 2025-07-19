using AutoMapper;
using Shared.DataAccess;
using Shared.DataAccess.Commands;
using Shared.DataAccess.Common;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Application.Extensions;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.UserPortfolio.Commands.Command;
using UserPortfolioService.UserPortfolio.Dtos;
using UserPortfolioService.UserPortfolio.Rules;

namespace UserPortfolioService.UserPortfolio.Commands.Handler;

public class AddImageCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IUserPortfolioRepository repository,
    IMapper mapper) : CommandHandler<AddImageCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(AddImageCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await repository.GetUserPortfolioWithIncludesByUserId(request.userId);
        userPortfolioEntity.EnsureNotNull();
        userPortfolioEntity.Image = request.image;
        var response = await repository.ModifyEntityAsync(userPortfolioEntity);
        var resultDto = mapper.Map<UserPortfolioDto>(response);
        return Result<UserPortfolioDto, Error>.Success(resultDto);
    }
}