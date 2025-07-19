using UserPortfolioService.Domain.Entities;

namespace UserPortfolioService.UserPortfolio.Dtos;

public class UserPortfolioDto
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string DateOfBirth { get; set; }
    public HashSet<CryptoWalletDto> CryptoWallets { get; set; }
    public TelegramDto? TelegramEntity { get; set; }
    public TwitterDto? TwitterEntity { get; set; }
    public ImageType? Image { get; set; }
}