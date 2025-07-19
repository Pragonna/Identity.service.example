using CryptoTransactionService.Library.Models;
using EventBus.EventBus.Base.Events;

namespace CryptoTransactionService.Library.Events;

public record CryptoAccountDataIntegrationEvent(Balance balance) : IntegrationEvent;