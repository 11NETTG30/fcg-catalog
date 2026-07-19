using FCGCatalog.Infrastructure.Messaging.Setup;
using FCGCatalog.Infrastructure.Persistence;
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
			x.AddEntityFrameworkOutbox<CatalogoDbContext>(o =>
			{
				o.UsePostgres();
				o.UseBusOutbox();
			});

			x.AddConsumers(typeof(RabbitMqConfiguration).Assembly);

			x.AddConfigureEndpointsCallback((context, name, cfg) =>
			{
				cfg.UseEntityFrameworkOutbox<CatalogoDbContext>(context);
				cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(1)));
			});

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