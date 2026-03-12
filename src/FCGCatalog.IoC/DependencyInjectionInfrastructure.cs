using FCGCatalog.Application.Identidade.Security;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Infrastructure.Identidade.Configurations;
using FCGCatalog.Infrastructure.Persistence.Repositories;
using FCGCatalog.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace FCGCatalog.IoC;

public static class DependencyInjectionInfrastructure
{
    extension(IServiceCollection services)
    {
        internal void AddInfrastructure()
        {
            services.AddRepositories();

            services.AddSingleton(typeof(IDomainLogger<>), typeof(DomainLogger<>));
            services.AddSingleton<ITokenSettings>(provider =>
            {
                JwtSettings jwtSettings = provider.GetRequiredService<IOptions<JwtSettings>>().Value;
                return jwtSettings;
            });
        }

        private void AddRepositories()
        {
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IBibliotecaUsuarioRepository, BibliotecaUsuarioRepository>();
        }
    }
}