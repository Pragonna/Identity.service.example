using System.Diagnostics.Contracts;
using Shared.DataAccess;
using Shared.DataAccess.Common;

namespace UserPortfolioService.Domain.Entities;

public class UserPortfolioEntity : Entity
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string DateOfBirth { get; set; }
    public ICollection<CryptoWalletEntity> CryptoWallets { get; set; }
    public TelegramEntity? TelegramEntity { get; set; }
    public TwitterEntity? TwitterEntity { get; set; }
    public ImageType? Image { get; set; }

    public UserPortfolioEntity()
    {
        CryptoWallets = new HashSet<CryptoWalletEntity>();
    }

    public UserPortfolioEntity(string userId, string firstName, string lastName, string gender, string dateOfBirth,
        string email) :
        this()
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Email = email;
    }
}