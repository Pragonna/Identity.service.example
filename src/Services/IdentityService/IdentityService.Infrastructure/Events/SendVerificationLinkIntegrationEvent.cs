using EventBus.EventBus.Base.Events;

namespace IdentityService.Infrastructure.Events;

public record SendVerificationLinkIntegrationEvent(string email,string link) : IntegrationEvent;