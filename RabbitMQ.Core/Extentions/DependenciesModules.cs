using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using RabbitMQ.Core.Service;
namespace RabbitMQ.Core.Extentions
{
    public static class DependenciesModules
    {
        public static IServiceCollection AddDependenciesCore(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IRabbitMQService, RabbitMQService>();


            return service;
        }
    }
}
