namespace IdentityService.Application.Exceptions;

public class OtpCodeIncorrectException : Exception
{
    public OtpCodeIncorrectException() : base("Otp code is incorrect")
    {
    }
}