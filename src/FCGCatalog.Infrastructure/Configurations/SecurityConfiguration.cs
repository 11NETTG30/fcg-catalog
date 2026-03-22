using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace FCGCatalog.Infrastructure.Configurations;

public static class SecurityConfiguration
{
    public static IServiceCollection ConfigureSecurity(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
		services.AddHttpContextAccessor();
        services.ConfigureJwtAuthentication(configuration);

		return services;
    }

	public static IServiceCollection ConfigureJwtAuthentication(
		this IServiceCollection services,
		IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Jwt:Authority"];
                options.Audience = configuration["Jwt:Audiencia"];
                options.RequireHttpsMetadata = false;
                options.MapInboundClaims = false;
                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
			});

        services.AddAuthorization();
        return services;
    }
}
