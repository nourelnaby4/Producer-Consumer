namespace RabbitMQ.Consumer.Service
{
    public interface IConsumerService
    {
        Task ReadMessage();
    }
}
