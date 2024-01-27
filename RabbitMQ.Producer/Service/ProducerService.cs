using RabbitMQ.Client;
using RabbitMQ.Core.Service;

namespace RabbitMQ.Producer.Service
{
    public class ProducerService : IProducerService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private static readonly string rouingkey = "message-key";
        private static readonly string queue = "message-queue";
        private static readonly string exchange = "message-exchange";
        
        public ProducerService(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }
        public void Send(string message)
        {
            //: TODO bussiness logic
            var (_, model)= _rabbitMQService.CreateConnection();
            _rabbitMQService.DeclareQuorumQueue(model,queue,exchange,ExchangeType.Direct,rouingkey);
          
            _rabbitMQService.SendMessage(model,exchange,rouingkey,message);
        }
    }
}
