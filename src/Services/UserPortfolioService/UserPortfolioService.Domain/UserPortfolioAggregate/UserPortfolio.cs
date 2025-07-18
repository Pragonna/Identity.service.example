using System.Net.Mime;
using IdentityService.Application.Exceptions;
using Shared.DataAccess;
using UserPortfolioService.Domain.Entities;
using UserPortfolioService.Domain.Exceptions;

namespace UserPortfolioService.Domain.UserPortfolioAggregate;

public sealed class UserPortfolio : AggregateRoot<UserPortfolio>
{
    private UserPortfolio()
    {
        CryptoWallets = new HashSet<CryptoWallet>();
    }

    private UserPortfolio(string userId, string firstName, string lastName, string gender, string dateOfBirth,
        string email) : this()
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Email = email;
    }

    public string UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Gender { get; private set; }
    public string DateOfBirth { get; private set; }
    public ICollection<CryptoWallet> CryptoWallets { get; private set; }
    public Telegram? Telegram { get; private set; }
    public Twitter? Twitter { get; private set; }
    public ImageType? Image { get; private set; }

    public static UserPortfolio Create(string userId, string firstName, string lastName, string gender,
        string dateOfBirth, string email)
    {
        return new UserPortfolio(userId, firstName, lastName, gender, dateOfBirth, email);
    }

    public UserPortfolio UpdateUserPortfolio(UserPortfolio userPortfolio)
    {
        EnsurePropertiesNullable(userPortfolio);
        return userPortfolio;
    }

    public void AddCryptoWallet(CryptoWallet cryptoWallet)
    {
        CryptoWallets.Add(cryptoWallet);
    }

    public void RemoveCryptoWallet(CryptoWallet cryptoWallet)
    {
        CryptoWallets.Remove(cryptoWallet);
    }

    public void AddImage(ImageType image)
    {
        if (!Image.IsValidSize) throw new ImageSizeLimitExceededException();
        Image = image;
    }

    public void RemoveImage() => Image = null;

    public void RemoveAllCryptoWallets() => CryptoWallets.Clear();

    public void AddTelegram(Telegram telegram) => Telegram = telegram;

    public void RemoveTelegram() => Telegram = null;

    public void AddTwitter(Twitter twitter) => Twitter = twitter;

    public void RemoveTwitter() => Twitter = null;

    private void EnsurePropertiesNullable(UserPortfolio userPortfolio)
    {
        if (userPortfolio.UserId == null) throw new UserNotFoundException();
        if (userPortfolio.FirstName == null) userPortfolio.FirstName = FirstName;
        if (userPortfolio.LastName == null) userPortfolio.LastName = LastName;
        if (userPortfolio.Gender == null) userPortfolio.Gender = Gender;
        if (userPortfolio.DateOfBirth == null) userPortfolio.DateOfBirth = DateOfBirth;
    }
}