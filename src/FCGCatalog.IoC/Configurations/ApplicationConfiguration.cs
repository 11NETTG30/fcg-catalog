using FCGCatalog.Application.Behaviors;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
using FCGCatalog.IoC;
using FCGCatalog.IoC.Configurations;
using FCGCatalog.IoC.Padroes.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace FCGCatalog.IoC.Configurations;

public static class ApplicationConfiguration
{
    extension(IServiceCollection services)
    {
        internal void ConfigureApplication()
        {
            services.ConfigurareMidiatR();
			services.ConfigureFluentValidation();
        }

        private void ConfigurareMidiatR()
        {
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(CriarJogoHandler).Assembly);
			});

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
		}

		private void ConfigureFluentValidation()
		{
			services.AddValidatorsFromAssemblyContaining<CriarJogoValidator>();
			services.AddFluentValidationClientsideAdapters();

			ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-Br");
			ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
		}
	}

}