using MassTransit;

namespace Messaging.Publishers
{
    public class EventPublisher<TEvent, TBus>(TBus bus) : IEventPublisher<TEvent> where TBus : IBus
	{
		private readonly TBus _bus = bus;

        public Task PublishAsync(TEvent eventData, CancellationToken cancellationToken = default)
		{
			if (eventData is null)
			{
				throw new ArgumentNullException(nameof(eventData));
			}

			return _bus.Publish(eventData, cancellationToken);
		}
	}
}