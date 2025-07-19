using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Repositories;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Infrastructure.Persistence.Context;
using UserPortfolioService.UserPortfolio.Commands.Command;

namespace UserPortfolioService.Infrastructure.Persistence.Repositories;

public class UserPortfolioRepository : BaseRepository<UserPortfolioEntity, UserPortfolioDbContext>,
    IUserPortfolioRepository
{
    private readonly UserPortfolioDbContext _context;

    public UserPortfolioRepository(UserPortfolioDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserPortfolioEntity> AddCryptoWalletUserPortfolio(UserPortfolioEntity userPortfolio)
    {
        _context.Entry(userPortfolio.CryptoWallets.First()).State = EntityState.Added;
        return await ModifyEntityAsync(userPortfolio);
    }

    public async Task<UserPortfolioEntity> AddTelegramUserPortfolio(UserPortfolioEntity userPortfolio)
    {
        _context.Entry(userPortfolio.TelegramEntity).State = EntityState.Added;
        return await ModifyEntityAsync(userPortfolio);
    }

    public async Task<UserPortfolioEntity> AddTwitterUserPortfolio(UserPortfolioEntity userPortfolio)
    {
        _context.Entry(userPortfolio.TwitterEntity).State = EntityState.Added;
        return await ModifyEntityAsync(userPortfolio);
    }

    public async Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByEmail(string email)
    {
        return await _context.UserPortfolios.Where(p => p.Email == email)
            .Include(u => u.CryptoWallets)?
            .Include(u => u.TelegramEntity)?
            .Include(u => u.TwitterEntity)?
            .Include(u => u.Image)?
            .FirstOrDefaultAsync();
    }

    public async Task<UserPortfolioEntity> GetUserPortfolioWithIncludesByUserId(string userId)
    {
        return await _context.UserPortfolios.Where(p => p.UserId == userId)
            .Include(u => u.CryptoWallets)?
            .Include(u => u.TelegramEntity)?
            .Include(u => u.TwitterEntity)?
            .Include(u => u.Image)?
            .FirstOrDefaultAsync();
    }

    public async Task<UserPortfolioEntity> RemoveCryptoWalletUserPortfolio(UserPortfolioEntity
        userPortfolio)
    {
        _context.Entry(userPortfolio.CryptoWallets.First()).State = EntityState.Deleted;
        return await ModifyEntityAsync(userPortfolio);
    } 
    public async Task<UserPortfolioEntity> RemoveTelegramUserPortfolio(UserPortfolioEntity
        userPortfolio)
    {
        _context.Entry(userPortfolio.TelegramEntity).State = EntityState.Deleted;
        return await ModifyEntityAsync(userPortfolio);
    }
    
    public async Task<UserPortfolioEntity> RemoveTwitterUserPortfolio(UserPortfolioEntity
        userPortfolio)
    {
        _context.Entry(userPortfolio.TwitterEntity).State = EntityState.Deleted;
        return await ModifyEntityAsync(userPortfolio);
    }
   
}