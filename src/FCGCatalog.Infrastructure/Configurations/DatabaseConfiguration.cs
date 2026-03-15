using FCGCatalog.Infrastructure.Persistence;
using FCGCatalog.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
	extension(IServiceCollection services)
	{
		public void ConfigureDatabase(IConfiguration configuration)
		{
			services.AddSingleton<AuditoriaSaveChangesInterceptor>();
			services.ConfigureDatabasePostgreSQL<CatalogoDbContext>(configuration);
		}

		private void ConfigureDatabasePostgreSQL<T>(IConfiguration configuration) where T : DbContext
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
}
