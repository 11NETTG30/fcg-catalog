using FCGCatalog.Infrastructure.Persistence.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FCGCatalog.Infrastructure.Configurations;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services,
                                                IConfiguration configuration)
    {
        var settings = configuration
                        .GetSection("MongoDB")
                        .Get<MongoDbSettings>()
                        ?? throw new InvalidOperationException("MongoDB settings não configuradas.");

        services.AddSingleton<IMongoClient>(new MongoClient(settings.ConnectionString));
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return new MongoDbContext(client, settings.DatabaseName);
        });

        return services;
    }
}
