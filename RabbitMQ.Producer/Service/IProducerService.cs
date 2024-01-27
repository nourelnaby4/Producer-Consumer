namespace RabbitMQ.Producer.Service
{
    public interface IProducerService
    {
        void Send(string message);
    }
}
