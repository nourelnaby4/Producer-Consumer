
namespace RabbitMQ.Consumer.Service
{
    public class HostedService : BackgroundService
    {
        private readonly IConsumerService _consumerService;
        public HostedService(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ReadMessage();
        }
    }
}
