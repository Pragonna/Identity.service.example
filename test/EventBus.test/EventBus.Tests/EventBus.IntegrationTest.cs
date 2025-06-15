using EventBus.EventBus.Base.Abstraction;
using EventBus.EventBus.Base.Events;
using EventBus.Factory;
using EventBus.IntegrationTest.Events;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace EventBus.IntegrationTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Fact]
    public void EventBus_Publish_ShouldTriggerHandlerAndAllowResultVerification()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        string expectedResult = "test";
        string currentResult = null;
        var testEvent = new TestEvent(expectedResult);

        serviceCollection.AddSingleton<IEventBus>(provider =>
        {
            return EventBusFactory.Create(Config.GetConfigRabbitMQ, provider);
        });

        var mockHandler = new Mock<IIntegrationEventHandler<TestEvent>>();
        mockHandler.Setup(e => e.Handle(It.IsAny<TestEvent>()))
            .Returns((TestEvent e) =>
            {
                currentResult = e.message;
                return Task.FromResult(e.message);
            });

        serviceCollection.AddSingleton(mockHandler.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventBus = serviceProvider.GetRequiredService<IEventBus>();
        var eventHandler = serviceProvider.GetRequiredService<IIntegrationEventHandler<TestEvent>>();
        eventHandler.Handle(testEvent);

        // eventBus.Subscribe<TestEvent, IIntegrationEventHandler<TestEvent>>();
        eventBus.Publish(testEvent);

        eventBus.Should().NotBeNull();
        currentResult.Should().NotBeNullOrEmpty();
        currentResult.Should().Be(expectedResult);

    }
}