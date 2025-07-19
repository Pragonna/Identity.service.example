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

public class AddUserPortfolioCommandHandler(
    IUserPortfolioUnitOfWork unitOfWork,
    IUserPortfolioRepository userPortfolioRepository,
    IMapper mapper,
    UserPortfolioBusinessRules rules) : CommandHandler<AddUserPortfolioCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(AddUserPortfolioCommand request,
        CancellationToken cancellationToken)
    {
        var isExists = await rules.UserPortfolioCannotDuplicatedWhenInsert(request.userId);
        var userPortfolioEntity = mapper.Map<UserPortfolioEntity>(request);

        if (!isExists) userPortfolioEntity = await userPortfolioRepository.AddEntityAsync(userPortfolioEntity);

        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);
        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}