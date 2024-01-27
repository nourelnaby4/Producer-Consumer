using RabbitMQ.Client;
using RabbitMQ.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Service
{
    public interface IRabbitMQService
    {
        (IConnection, IModel) CreateConnection();
        ResultDto DeclareQuorumQueue(IModel model, string queue, string exchange, string exchangeType = ExchangeType.Fanout, string routingKey = "");
        void SendMessage<T>(IModel model, string exchange, string routingKey, T data, IBasicProperties basicProperties = null);
        Task<ResultDto> ConsumeMessage(IModel model, string queue, bool requeueWhenFailure, Func<string, ResultDto> processingFunc);
    }
}
