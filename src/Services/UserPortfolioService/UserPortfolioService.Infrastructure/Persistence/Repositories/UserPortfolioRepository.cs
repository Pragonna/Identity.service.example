using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.DataAccess.Repositories;
using UserPortfolioService.Application.Repositories;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.UserPortfolioAggregate;
using UserPortfolioService.Infrastructure.Persistence.Context;

namespace UserPortfolioService.Infrastructure.Persistence.Repositories;

public class UserPortfolioRepository : BaseRepository<UserPortfolioEntity, UserPortfolioDbContext>,
    IUserPortfolioRepository
{
    private readonly UserPortfolioDbContext _context;

    public UserPortfolioRepository(UserPortfolioDbContext context) : base(context)
    {
        _context = context;
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
    //
    // public async Task<CryptoWallet> RemoveCryptoWallet(CryptoWallet cryptoWallet)
    // {
    //     _context.Entry(cryptoWallet).State = EntityState.Deleted;
    //     return cryptoWallet;
    // }
    //
    // public async Task<Telegram> RemoveTelegram(Telegram telegram)
    // {
    //     _context.Entry(telegram).State = EntityState.Deleted;
    //     return telegram;
    // }
    //
    // public async Task<Twitter> RemoveTwitter(Twitter twitter)
    // {
    //     _context.Entry(twitter).State = EntityState.Deleted;
    //     return twitter;
    // }
    // public async Task<CryptoWallet> UpdateCryptoWallet(CryptoWallet cryptoWallet)
    // {
    //     _context.Entry(cryptoWallet).State = EntityState.Modified;
    //     return cryptoWallet;
    // }
    //
    // public async Task<Telegram> UpdateTelegram(Telegram telegram)
    // {
    //     _context.Entry(telegram).State = EntityState.Modified;
    //     return telegram;
    // }
    //
    // public async Task<Twitter> UpdateTwitter(Twitter twitter)
    // {
    //     _context.Entry(twitter).State = EntityState.Modified;
    //     return twitter;
    // }
}