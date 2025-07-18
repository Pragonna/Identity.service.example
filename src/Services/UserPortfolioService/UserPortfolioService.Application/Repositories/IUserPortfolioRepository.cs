using Shared.DataAccess.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.UserPortfolioAggregate;

namespace UserPortfolioService.Application.Repositories;

public interface IUserPortfolioRepository : IWriteRepository<UserPortfolioEntity>, IReadRespository<UserPortfolioEntity>
{
    Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByEmail(string email);
    Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByUserId(string userId);
    // Task<CryptoWallet> RemoveCryptoWallet(CryptoWallet cryptoWallet);
    // Task<Telegram> RemoveTelegram(Telegram telegram);
    // Task<Twitter> RemoveTwitter(Twitter twitter);
    // Task<CryptoWallet> UpdateCryptoWallet(CryptoWallet cryptoWallet);
    // Task<Telegram> UpdateTelegram(Telegram telegram);
    // Task<Twitter> UpdateTwitter(Twitter twitter);
}