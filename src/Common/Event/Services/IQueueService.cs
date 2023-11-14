namespace Event.Services
{
    public interface IQueueService
    {
        Task SendAsync<T>(T data, string queueName) where T : IEvent;
    }
}