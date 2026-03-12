using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.IoC;

public static class DependencyInjectionApplication
{
    extension(IServiceCollection services)
    {
        internal void AddApplication()
        {
            //services.AddSingleton<IEventBus, RabbitMqEventBus>();
        }
    }
}