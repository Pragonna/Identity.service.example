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

        var telegram = Telegram.Create(request.telegramDto.TelegramHandle, request.telegramDto.TelegramUserId,
            request.telegramDto.FullName, request.telegramDto.IsSubscribed);

        var userPortfolio = mapper.Map<Domain.UserPortfolioAggregate.UserPortfolio>(userPortfolioEntity);
        userPortfolio.AddTelegram(telegram);
        userPortfolioEntity = mapper.Map<UserPortfolioEntity>(userPortfolio);
        await userPortfolioRepository.ModifyEntityAsync(userPortfolioEntity);
        var userPortfolioDto = mapper.Map<UserPortfolioDto>(userPortfolioEntity);

        return Result<UserPortfolioDto, Error>.Success(userPortfolioDto);
    }
}