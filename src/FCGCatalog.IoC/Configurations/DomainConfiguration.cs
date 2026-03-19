using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.IoC.Configurations;

public static class DomainConfiguration
{
	public static IServiceCollection ConfigureDomain(
		this IServiceCollection services)
	{
		return services;
	}
}