namespace Messaging.Publishers
{
    public interface IEventPublisher<TEvent>
    {
        Task PublishAsync(TEvent eventData, CancellationToken cancellationToken = default);
    }
}