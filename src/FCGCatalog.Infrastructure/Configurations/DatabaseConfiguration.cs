using FCGCatalog.Infrastructure.Persistence;
using FCGCatalog.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
	public static IServiceCollection ConfigureDatabase(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddSingleton<AuditoriaSaveChangesInterceptor>();
		services.ConfigureDatabasePostgreSQL<CatalogoDbContext>(configuration);

		return services;
	}

	private static void ConfigureDatabasePostgreSQL<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
	{
		string? connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<T>((serviceProvider, options) =>
			options
				.UseNpgsql(connectionString, optionsPostgress =>
				{
					optionsPostgress.MigrationsHistoryTable("__ef_migrations_history");
				})
				.AddInterceptors(serviceProvider.GetRequiredService<AuditoriaSaveChangesInterceptor>())
		);
	}
}
