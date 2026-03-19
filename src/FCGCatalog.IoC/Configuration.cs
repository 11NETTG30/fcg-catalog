using FCGCatalog.IoC.Configurations;
using Microsoft.Extensions.Hosting;

namespace FCGCatalog.IoC;

public static class Configuration
{
	public static IHostApplicationBuilder RegisterIoCConfigurations(
		this IHostApplicationBuilder builder)
	{
		builder.Configuration.ConfigureEnvironment();
		builder.Services.ConfigureDomain();
		builder.Services.ConfigureApplication();
		builder.Services.ConfigureInfrastructure(builder.Configuration);

		return builder;
	}
}