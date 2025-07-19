using Shared.DataAccess.Repositories;
using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.Application.Repositories;

public interface IUserPortfolioRepository : IWriteRepository<UserPortfolioEntity>, IReadRespository<UserPortfolioEntity>
{
    Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByEmail(string email);
    Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByUserId(string userId);
    Task<UserPortfolioEntity> AddCryptoWalletUserPortfolio(UserPortfolioEntity userPortfolio);
    Task<UserPortfolioEntity> AddTelegramUserPortfolio(UserPortfolioEntity userPortfolio);
    Task<UserPortfolioEntity> AddTwitterUserPortfolio(UserPortfolioEntity userPortfolio);
    Task<UserPortfolioEntity> RemoveCryptoWalletUserPortfolio(UserPortfolioEntity userPortfolio);
    Task<UserPortfolioEntity> RemoveTelegramUserPortfolio(UserPortfolioEntity userPortfolio);
    Task<UserPortfolioEntity> RemoveTwitterUserPortfolio(UserPortfolioEntity userPortfolio);
}