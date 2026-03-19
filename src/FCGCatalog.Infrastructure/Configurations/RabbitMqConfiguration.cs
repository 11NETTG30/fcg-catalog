using FCGCatalog.Infrastructure.Messaging.Setup;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.Infrastructure.Configurations;

public static class RabbitMqConfiguration
{
	public static IServiceCollection ConfigureMessaging(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

		var rabbit = configuration
			.GetSection("RabbitMQ")
			.Get<RabbitMqSettings>()!;

		services.AddMassTransit(x =>
		{
			x.AddConsumers(typeof(RabbitMqConfiguration).Assembly);

			x.UsingRabbitMq((context, cfg) =>
			{
				cfg.MessageTopology.SetEntityNameFormatter(new CustomNameEntityNameFormatter());

				cfg.Host(
					rabbit.Host,
					rabbit.VirtualHost,
					h =>
					{
						h.Username(rabbit.Username);
						h.Password(rabbit.Password);
					});

				cfg.ConfigureEndpoints(context);
			});
		});

		return services;
	}
}