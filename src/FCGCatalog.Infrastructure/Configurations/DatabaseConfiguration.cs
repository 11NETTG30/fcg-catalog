using FCGCatalog.Infrastructure.Persistence;
using FCGCatalog.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

	public static async Task AplicarMigracoesAsync(this WebApplication app)
	{
		const int maxTentativas = 10;
		const int esperaSegundos = 5;

		using var scope = app.Services.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<CatalogoDbContext>();
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<CatalogoDbContext>>();

		for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
		{
			try
			{
				logger.LogInformation("Aplicando migrations (tentativa {Tentativa}/{Max})...", tentativa, maxTentativas);
				await db.Database.MigrateAsync();
				logger.LogInformation("Migrations aplicadas com sucesso.");
				return;
			}
			catch (Exception ex) when (tentativa < maxTentativas)
			{
				logger.LogWarning(ex, "Banco indisponível. Aguardando {Segundos}s antes de tentar novamente...", esperaSegundos);
				await Task.Delay(TimeSpan.FromSeconds(esperaSegundos));
			}
		}
	}
}
