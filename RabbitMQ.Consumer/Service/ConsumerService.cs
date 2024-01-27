using RabbitMQ.Client;
using RabbitMQ.Core.Service;
using RabbitMQ.Service.Dto;

namespace RabbitMQ.Consumer.Service
{
    public class ConsumerService : IConsumerService
    {
        private readonly IRabbitMQService _rabbiService;
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _model;
        private static readonly string queue = "message-queue";
        public ConsumerService(
            IRabbitMQService rabbiService,
            ILogger<ConsumerService> logger)
        {
            _rabbiService = rabbiService;
            _logger = logger;
            (_connection, _model) = _rabbiService.CreateConnection();

        }
        public async Task ReadMessage()
        {
            await _rabbiService.ConsumeMessage(model: _model, queue: queue, requeueWhenFailure: true, (message) =>
            {
                _logger.LogInformation(message);
                return new ResultDto { IsValid = true };

            });
        }
    }
}
