namespace UserPortfolioService.Exceptions;

public class CryptoWalletAlreadyExistsException : Exception
{
    public CryptoWalletAlreadyExistsException() : base("CryptoWallet already exists")
    {
    }
}

public class TelegramAlreadyExistsException : Exception
{
    public TelegramAlreadyExistsException() : base("Telegram already exists")
    {
    }
}

public class TwitterAlreadyExistsException : Exception
{
    public TwitterAlreadyExistsException() : base("Twitter already exists")
    {
    }
}