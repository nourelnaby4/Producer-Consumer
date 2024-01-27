using RabbitMQ.Client;
using RabbitMQ.Core.Service;
using RabbitMQ.Service.Dto;

namespace RabbitMQ.Consumer.Service
{ 
    public class ConsumerService : IConsumerService
    {
        private readonly RabbitMQService _rabbiService;
        private readonly IConnection _connection;
        private readonly IModel _model;
        private static readonly string queue = "message-queue";
        public ConsumerService(
            RabbitMQService rabbiService,
            IConnection connection,
            IModel model) 
        {
            _rabbiService = rabbiService;
            (_connection, _model) =_rabbiService.CreateConnection();

        }
        public async Task ReadMessage()
        {
            await _rabbiService.ConsumeMessage(model:_model, queue:queue,  requeueWhenFailure:true, (message) =>
            {
                Console.WriteLine(message);
                return new ResultDto { IsValid = true };

            });
        }
    }
}
