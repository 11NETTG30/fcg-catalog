using FCGCatalog.IoC.Configurations;
using Microsoft.Extensions.Hosting;

namespace FCGCatalog.IoC;
public static class Configuration
{
    extension(IHostApplicationBuilder builder)
    {
        public void RegisterIoCConfigurations()
        {
			builder.Configuration.ConfigureEnvironment();
            builder.Services.ConfigureDomain();
			builder.Services.ConfigureApplication();
			builder.Services.ConfigureInfrastructure(builder.Configuration);
        }
	}
}