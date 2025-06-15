using EventBus.EventBus.Base.Events;

namespace EmailSender.Service.Events.SendOtp;

public record SendOtpVerificationEmailIntegrationEvent(string email, string otp) : IntegrationEvent;