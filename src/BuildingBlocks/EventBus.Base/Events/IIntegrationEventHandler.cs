namespace EventBus.EventBus.Base.Events;

public interface IIntegrationEventHandler<TEvent>
    where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event);
}