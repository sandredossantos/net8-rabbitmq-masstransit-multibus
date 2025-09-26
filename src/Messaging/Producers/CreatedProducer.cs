using Messaging.Events;
using Messaging.Publishers;

namespace Messaging.Producers
{
    public interface ICreatedProducer
    {
        Task ProduceAsync(CreatedEvent createdEvent, CancellationToken cancellationToken = default);
    }

    public class CreatedProducer(IEventPublisher<CreatedEvent> publisher) : ICreatedProducer
    {
        private readonly IEventPublisher<CreatedEvent> publisher = publisher;

        public async Task ProduceAsync(CreatedEvent createdEvent, CancellationToken cancellationToken = default)
        {
            await publisher.PublishAsync(createdEvent, cancellationToken);
        }
    }
}
