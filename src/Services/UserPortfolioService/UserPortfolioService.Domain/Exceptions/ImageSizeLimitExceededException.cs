namespace UserPortfolioService.Domain.Exceptions;

public class ImageSizeLimitExceededException : Exception
{
    public ImageSizeLimitExceededException()
        : base("Image size must not exceed 10 MB.") { }

    public ImageSizeLimitExceededException(string message)
        : base(message) { }

    public ImageSizeLimitExceededException(string message, Exception inner)
        : base(message, inner) { }
}