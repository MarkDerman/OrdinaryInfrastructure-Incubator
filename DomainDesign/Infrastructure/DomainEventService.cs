using Microsoft.Extensions.Logging;
using Odin.Logging;

namespace Odin.DomainDesign;

public class DomainEventService : IDomainEventService
{
    private readonly ILoggerWrapper<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    public DomainEventService(ILoggerWrapper<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Publish(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent), cancellationToken);
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        var result = Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        if (result == null)
            throw new Exception($"Unable to create DomainEventNotification<{domainEvent.GetType().Name}>");

        return (INotification)result;
    }
}