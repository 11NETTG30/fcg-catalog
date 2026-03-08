using Microsoft.Extensions.DependencyInjection;
namespace FCGCatalog.IoC;
public static class DependencyInjectionDomain
{
    extension(IServiceCollection services)
    {
        internal void AddDomain()
        {
            services.AddScoped<IRefreshTokenDomainService, RefreshTokenDomainService>();
        }
    }
}