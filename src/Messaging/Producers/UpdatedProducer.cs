using Messaging.Events;
using Messaging.Publishers;

namespace Messaging.Producers
{
    public interface IUpdatedProducer
    {
        Task ProduceAsync(UpdatedEvent updatedEvent, CancellationToken cancellationToken = default);
    }

    public class UpdatedProducer(IEventPublisher<UpdatedEvent> publisher) : IUpdatedProducer
    {
        private readonly IEventPublisher<UpdatedEvent> publisher = publisher;

        public async Task ProduceAsync(UpdatedEvent @event, CancellationToken cancellationToken = default)
        {
            await publisher.PublishAsync(@event, cancellationToken);
        }
    }
}
