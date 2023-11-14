using MassTransit;

namespace Event.Services
{
    public class QueueService : IQueueService
    {
        private readonly ISendEndpointProvider _provider;

        public QueueService(ISendEndpointProvider provider)
        {
            _provider = provider;
        }

        public async Task SendAsync<T>(T data, string queueName) where T : IEvent
        {
            var endpoint = await _provider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endpoint.Send(data);
        }
    }
}