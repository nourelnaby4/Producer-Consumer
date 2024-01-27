﻿using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumer.Service;
namespace RabbitMQ.Consumer.Extentions
{
    public static class DependenciesModules
    {
        public static IServiceCollection AddDependenciesConsumer(this IServiceCollection service)
        {
            service.AddScoped<IConsumerService, ConsumerService>();
            service.AddHostedService<HostedService>();

            return service;
        }
    }
}
