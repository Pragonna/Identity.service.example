using EmailSender.Service.Enums;
using EventBus.EventBus.Base.Events;

namespace EmailSender.Service.Events.SendVerificationLink;

public class
    SendVerificationLinkIntegrationEventHandler(IEmailSender emailSender) : IIntegrationEventHandler<SendVerificationLinkIntegrationEvent>
{
    public async Task Handle(SendVerificationLinkIntegrationEvent @event)
    {
        await emailSender.SendEmailAsync(@event.email, "Verification link", @event.link, MessageType.VerificationLink);
        Console.WriteLine("Message has been sent successfully..");
    }
}