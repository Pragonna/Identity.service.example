# EventBus.Base

`EventBus.Base` is a foundational library for handling integration events in distributed systems. It provides a clean, extensible base that supports various messaging technologies such as RabbitMQ, Azure Service Bus, or Kafka by abstracting common event bus logic.

---

##    Features

- **Configurable Event Naming**  
  Add/remove prefixes and suffixes to event names for consistency and flexibility.

- **Centralized Subscription Management**  
  Manage event subscriptions and handler types in one place.

- **Generic Event Handling**  
  Use strongly typed handlers via `IIntegrationEventHandler<T>` for safer and cleaner code.

- **Dependency Injection Friendly**  
  Easily integrates with `Microsoft.Extensions.DependencyInjection`.

- **Abstract Base Class**  
  Inherit from `BaseEventBus` to build custom event bus implementations.

---

##   Getting Started

### 🔧 Installation

This project is intended to be referenced as a core library in your application. Compile and add as a reference or publish as a NuGet package.

---

### ⚙️ Configuration

Use the `EventBusConfig` class to configure your event bus.

```csharp
public class EventBusConfig
{
    public int ConnectionRetryCount { get; set; } = 5;
    public string EventBusConnectionString { get; set; }
    public string SubscriberClientAppName { get; set; } = "YourAppName";
    public string EventNamePrefix { get; set; } = "IntegrationEvent";
    public string EventNameSuffix { get; set; } = "";
    public bool DeleteEventPrefix { get; set; } = false;
    public bool DeleteEventSuffix { get; set; } = false;
    public EventBusType EventBusType { get; set; } = EventBusType.RabbitMQ;
}
```

---

## 🛠 Usage

### 1. Define Integration Events

```csharp
public class ProductCreatedIntegrationEvent : IntegrationEvent
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
```

### 2. Define Event Handlers

```csharp
public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    public Task Handle(ProductCreatedIntegrationEvent @event)
    {
        Console.WriteLine($"Product Created: {@event.ProductName} (ID: {@event.ProductId})");
        return Task.CompletedTask;
    }
}
```

### 3. Register Services

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<EventBusConfig>(sp => new EventBusConfig
    {
        SubscriberClientAppName = "OrderService",
        // Additional config...
    });

    // Example:
    // services.AddSingleton<IEventBus, RabbitMQEventBus>();

    services.AddTransient<ProductCreatedIntegrationEventHandler>();
}
```

### 4. Subscribe to Events

```csharp
public class Startup
{
    public void Configure(IApplicationBuilder app, IEventBus eventBus)
    {
        eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();
    }
}
```

### 5. Publish Events

```csharp
public class ProductService
{
    private readonly IEventBus _eventBus;

    public ProductService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task CreateProduct(Product product)
    {
        var productCreatedEvent = new ProductCreatedIntegrationEvent
        {
            ProductId = product.Id,
            ProductName = product.Name
        };

        await _eventBus.Publish(productCreatedEvent);
    }
}
```

---

##  Extending BaseEventBus

To build your own implementation (e.g., for RabbitMQ):

```csharp
public class RabbitMQEventBus : BaseEventBus
{
    public RabbitMQEventBus(IServiceProvider serviceProvider, EventBusConfig config)
        : base(serviceProvider, config)
    {
        // Initialize RabbitMQ connection
    }

    public override Task Publish(IntegrationEvent @event)
    {
        // RabbitMQ publish logic
        return Task.CompletedTask;
    }

    public override Task Subscribe<T, THandle>()
    {
        // RabbitMQ subscribe logic
        return Task.CompletedTask;
    }

    public override Task UnSubscribe<T, THandle>()
    {
        // RabbitMQ unsubscribe logic
        return Task.CompletedTask;
    }
}
```

---

##  Supported Event Bus Types

You can define and extend `EventBusType` enum to support different messaging backends:
- `RabbitMQ`
- `AzureServiceBus`
- `Kafka`


---

##  License

Microservices Architecture for Containerized .NET applications

---

##  Contributions

Pull requests are welcome! Please open an issue first to discuss any major changes.

---

##  Contact

For questions or suggestions, feel free to reach out or create an issue in the repository.
mail: pragonna.example@gmail.com