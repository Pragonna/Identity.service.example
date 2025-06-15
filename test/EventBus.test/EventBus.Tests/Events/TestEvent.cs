using EventBus.EventBus.Base.Events;

namespace EventBus.IntegrationTest.Events;

public record TestEvent(string message) : IntegrationEvent
{
}