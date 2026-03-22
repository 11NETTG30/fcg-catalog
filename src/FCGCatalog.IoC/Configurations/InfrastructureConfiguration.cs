using FCGCatalog.Application.Abstractions.Messaging;
using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Infrastructure.Configurations;
using FCGCatalog.Infrastructure.Messaging;
using FCGCatalog.Infrastructure.Persistence.Repositories;
using FCGCatalog.Infrastructure.Shared;
using FCGCatalog.Infrastructure.Shared.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.IoC.Configurations;

public static class InfrastructureConfiguration
{
	public static IServiceCollection ConfigureInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.ConfigureJwtAuthentication(configuration);
		services.ConfigureMessaging(configuration);
		services.ConfigureDatabase(configuration);
		services.ConfigureRepositories();

		services.AddScoped<IEventPublisher, EventPublisherMassTransit>();
		services.AddScoped<IUsuarioContexto, UsuarioContexto>();
		services.AddSingleton(typeof(IDomainLogger<>), typeof(DomainLogger<>));

		return services;
	}

	private static IServiceCollection ConfigureRepositories(
		this IServiceCollection services)
	{
		services.AddScoped<IJogoRepository, JogoRepository>();
		services.AddScoped<IBibliotecaUsuarioRepository, BibliotecaUsuarioRepository>();

		return services;
	}
}