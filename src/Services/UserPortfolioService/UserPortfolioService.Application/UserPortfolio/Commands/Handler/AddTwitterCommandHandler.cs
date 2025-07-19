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

public class AddTwitterCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    IUserPortfolioRepository userPortfolioRepository,
    UserPortfolioBusinessRules rules) : CommandHandler<AddTwitterCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(AddTwitterCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity = await
            rules.TwitterCannotDuplicatedWhenInsertOrUpdated(request.userId, request.twitterDto.TwitterHandle);

        var twitter = mapper.Map<TwitterEntity>(request.twitterDto);

        userPortfolioEntity.TwitterEntity = twitter;
        await userPortfolioRepository.AddTwitterUserPortfolio(userPortfolioEntity);
        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}