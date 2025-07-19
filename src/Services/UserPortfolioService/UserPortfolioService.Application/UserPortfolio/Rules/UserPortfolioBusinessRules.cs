using IdentityService.Application.Exceptions;
using IdentityService.Application.Repositories;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Exceptions;
using UserPortfolioService.UserPortfolio.Dtos;

namespace UserPortfolioService.UserPortfolio.Rules;

public class UserPortfolioBusinessRules(IUserPortfolioRepository userPortfolioRepositoryRepository)
{
    public async Task<UserPortfolioEntity> UserPortfolioCannotDuplicatedWhenInsert(string userId)
    {
        var user = await userPortfolioRepositoryRepository.GetUserPortfolioWithIncludesByUserId(userId);
        return user;
    }

    public async Task<UserPortfolioEntity> CryptoWalletCannotDuplicatedWhenInsertOrUpdated(string userId,
        string walletAddress)
    {
        var userPortfolio = await userPortfolioRepositoryRepository.GetUserPortfolioWithIncludesByUserId(userId);
        if (userPortfolio == null) throw new UserNotFoundException();
        if (userPortfolio.CryptoWallets.Where(w => w.WalletAddress == walletAddress).Any())
            throw new CryptoWalletAlreadyExistsException();

        return userPortfolio;
    }

    public async Task<UserPortfolioEntity> TelegramCannotDuplicatedWhenInsertOrUpdated(string userId,
        long telegramUserId)
    {
        var userPortfolio = await userPortfolioRepositoryRepository.GetUserPortfolioWithIncludesByUserId(userId);
        if (userPortfolio == null) throw new UserNotFoundException();
        if (userPortfolio.TelegramEntity != null && userPortfolio.TelegramEntity.TelegramUserId == telegramUserId)
            throw new TelegramAlreadyExistsException();

        return userPortfolio;
    }

    public async Task<UserPortfolioEntity> TwitterCannotDuplicatedWhenInsertOrUpdated(string userId,
        string twitterHandle)
    {
        var userPortfolio = await userPortfolioRepositoryRepository.GetUserPortfolioWithIncludesByUserId(userId);
        if (userPortfolio == null) throw new UserNotFoundException();
        if (userPortfolio.TwitterEntity != null && userPortfolio.TwitterEntity.TwitterHandle == twitterHandle)
            throw new TwitterAlreadyExistsException();

        return userPortfolio;
    }
}