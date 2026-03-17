using FCGCatalog.Application.Abstractions.Messaging;
using MassTransit;

namespace FCGCatalog.Infrastructure.Messaging;

public class EventPublisherMassTransit : IEventPublisher
{
	private readonly IPublishEndpoint _publishEndpoint;

	public EventPublisherMassTransit(IPublishEndpoint publishEndpoint)
	{
		_publishEndpoint = publishEndpoint;
	}

	public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
	{
		return _publishEndpoint.Publish(message, cancellationToken);
	}
}
