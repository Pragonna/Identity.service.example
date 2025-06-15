using EventBus.EventBus.Base.Events;

namespace EmailSender.Service.Events.SendVerificationLink;

public record SendVerificationLinkIntegrationEvent(string email,string link) : IntegrationEvent;