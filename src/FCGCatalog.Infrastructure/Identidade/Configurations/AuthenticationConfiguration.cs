using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.Infrastructure.Identidade.Configurations;

public static class AuthenticationConfiguration
{
    extension(IServiceCollection services)
    {
        public void AddJwtAuthentication(IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Jwt:Authority"];
                    options.Audience = configuration["Jwt:Audiencia"];
                    options.RequireHttpsMetadata = false;
                    options.MapInboundClaims = false;
                });

            services.AddAuthorization();
        }
    }
}
