using EmailSender.Service.Enums;
using EventBus.EventBus.Base.Events;

namespace EmailSender.Service.Events.SendOtp;

public class SendOtpVerificationEmailIntegrationEventHandler(IEmailSender emailSender)
    : IIntegrationEventHandler<SendOtpVerificationEmailIntegrationEvent>
{
    public async Task Handle(SendOtpVerificationEmailIntegrationEvent @event)
    {
        //Send OTP code to email
        await emailSender.SendEmailAsync(@event.email, "OTP code", @event.otp, MessageType.Otp);
        Console.WriteLine("Message has been sent successfully..");
    }
}