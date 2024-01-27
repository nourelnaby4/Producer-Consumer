using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Service.Dto;
using System.Text;
using System.Text.Json;


namespace RabbitMQ.Core.Service
{
    public class RabbitMQService<T> where T : class
    {
        
        public (IConnection, IModel) CreateConnection()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                UserName = "gust",
                Password = "gust",
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            var model = channel.CreateModel();

            return (channel, model);
        }
        public ResultDto DeclareQuorumQueue(IModel model, string queue, string exchange, string exchangeType = ExchangeType.Fanout, string routingKey = "")
        {
            var result = new ResultDto();
            try
            {
                Dictionary<string, object> argument = new();
                argument.Add("x-queue-type", "quorum");
                model.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, argument);
                model.ExchangeDeclare(exchange: exchange, type: exchangeType, durable: true, autoDelete: false);
                model.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);

                result.IsValid = true;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public void SendMessage(IModel model, string exchange, string routingKey, T data, IBasicProperties basicProperties = null)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
            model.BasicPublish(exchange: exchange, routingKey: routingKey, body: body, basicProperties: basicProperties);
        }

        public async Task<ResultDto> ConsumeMessage(IModel model, string queue, bool requeueWhenFailure, Func<string, ResultDto> processingFunc)
        {
            ResultDto result = new();

            try
            {
                var consumer = new AsyncEventingBasicConsumer(model);
                consumer.Received += async (ch, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var processingResult = processingFunc(message);
                    await Task.CompletedTask;

                    if (processingResult.IsValid)
                        model.BasicAck(ea.DeliveryTag, false);
                    else
                        model.BasicNack(ea.DeliveryTag, false, requeueWhenFailure);
                };

                model.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
                await Task.CompletedTask;

                result.IsValid = true;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
