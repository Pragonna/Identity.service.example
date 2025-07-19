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

public class AddTelegramCommandHandler(
    IUserPortfolioRepository userPortfolioRepository,
    IUserPortfolioUnitOfWork unitOfWork,
    IMapper mapper,
    UserPortfolioBusinessRules rules) : CommandHandler<AddTelegramCommand, UserPortfolioDto>(unitOfWork)
{
    public override async Task<Result<UserPortfolioDto, Error>> ExecuteAsync(AddTelegramCommand request,
        CancellationToken cancellationToken)
    {
        var userPortfolioEntity =
            await rules.TelegramCannotDuplicatedWhenInsertOrUpdated(request.userId,
                request.telegramDto.TelegramUserId);

        var telegram = mapper.Map<TelegramEntity>(request.telegramDto);
        userPortfolioEntity.TelegramEntity = telegram;
        await userPortfolioRepository.AddTelegramUserPortfolio(userPortfolioEntity);
        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}