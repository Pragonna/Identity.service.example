using EmailSender.Service.Enums;

namespace EmailSender.Service;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string msg, MessageType type);
}