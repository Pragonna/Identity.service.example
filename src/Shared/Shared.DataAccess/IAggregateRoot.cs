using EventBus.EventBus.Base.Events;
using Shared.DataAccess.Common;

namespace Shared.DataAccess;

public interface IAggregateRoot
{
    IReadOnlyCollection<IntegrationEvent> IntegrationEvents { get; }
    IReadOnlyCollection<IntegrationEvent> PopDomainEvents();
    void ClearEvents();
}

public abstract class AggregateRoot<T> : IAggregateRoot
{
    private readonly IList<IntegrationEvent> _events = [];
    public IReadOnlyCollection<IntegrationEvent> IntegrationEvents => _events.AsReadOnly();

    public IReadOnlyCollection<IntegrationEvent> PopDomainEvents()
    {
        var events = _events.ToList();
        _events.Clear();
        return events;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }

    public void RaiseIntegrationEvent(IntegrationEvent @event)
    {
        _events.Add(@event);
    }
}