using FCGCatalog.Application.Behaviors;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
using FCGCatalog.IoC.Padroes.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace FCGCatalog.IoC.Configurations;

public static class ApplicationConfiguration
{
	public static IServiceCollection ConfigureApplication(
		this IServiceCollection services)
	{
		services.ConfigureMediatR();
		services.ConfigureFluentValidation();

		return services;
	}

	private static IServiceCollection ConfigureMediatR(
		this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(typeof(CriarJogoHandler).Assembly);
		});

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

		return services;
	}

	private static IServiceCollection ConfigureFluentValidation(
		this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<CriarJogoValidator>();
		services.AddFluentValidationClientsideAdapters();

		ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
		ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();

		return services;
	}
}