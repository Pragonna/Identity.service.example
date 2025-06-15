using EventBus.EventBus.Base.Events;

namespace IdentityService.Infrastructure.Events;

public record SendOtpVerificationEmailIntegrationEvent(string email, string otp) : IntegrationEvent;
