using System.Diagnostics;
using System.Net.Mail;
using EmailSender.Service.Enums;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EmailSender.Service;

public class EmailService : IEmailSender
{
    public async Task SendEmailAsync(string toEmail, string subject, string msg, MessageType type)
    {
        var smtpSettings = EmailConfig.LoadFromEnv();
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("My Company", smtpSettings.User));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        var textBody = type switch
        {
            MessageType.Otp => LoadOtpHtmlBody(msg),
            MessageType.VerificationLink => LoadVerificationLinkHtmlBody(msg)
        };

        message.Body = new TextPart("html")
        {
            Text = textBody
        };

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(smtpSettings.User, smtpSettings.Pass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string LoadOtpHtmlBody(string otp)
    {
        var html = File.ReadAllText(Directory.GetCurrentDirectory() + "/EmailTemplates/OtpTemplate.html");
        return html.Replace("{{OTP}}", otp);
    }

    private string LoadVerificationLinkHtmlBody(string verificationLink)
    {
        var html = File.ReadAllText(Directory.GetCurrentDirectory() + "/EmailTemplates/VerificationLinkTemplate.html");
        return html.Replace("{{VERIFIED_LINK}}", verificationLink);
    }
}