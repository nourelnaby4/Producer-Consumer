using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Producer.Service;
namespace RabbitMQ.Producer.Extentions
{
    public static class DependenciesModules
    {
        public static IServiceCollection AddDependenciesProducer(this IServiceCollection service)
        {
            service.AddScoped<IProducerService, ProducerService>();

            return service;
        }
    }
}
