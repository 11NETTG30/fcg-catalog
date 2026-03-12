using FCGCatalog.Application.Identidade.Security;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Infrastructure.Configurations;
using FCGCatalog.Infrastructure.Identidade.Configurations;
using FCGCatalog.Infrastructure.Persistence.Repositories;
using FCGCatalog.Infrastructure.Shared;
using FCGCatalog.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FCGCatalog.IoC.Configurations;

public static class InfrastructureConfiguration
{
    extension(IServiceCollection services)
    {
        internal void ConfigureInfrastructure(IConfiguration configuration)
		{
            services.AddDatabase(configuration);
			services.ConfigureRepositories();

            services.AddSingleton(typeof(IDomainLogger<>), typeof(DomainLogger<>));
            services.AddSingleton<ITokenSettings>(provider =>
            {
                JwtSettings jwtSettings = provider.GetRequiredService<IOptions<JwtSettings>>().Value;
                return jwtSettings;
            });
        }

        private void ConfigureRepositories()
        {
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IBibliotecaUsuarioRepository, BibliotecaUsuarioRepository>();
        }
    }
}