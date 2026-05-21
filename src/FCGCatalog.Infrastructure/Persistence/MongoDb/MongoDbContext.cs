using FCGCatalog.Infrastructure.Persistence.MongoDb.Configurations;
using MongoDB.Driver;

namespace FCGCatalog.Infrastructure.Persistence.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient client, string databaseName)
    {
        _database = client.GetDatabase(databaseName);
        ApplyConfigurations();
    }

    private void ApplyConfigurations()
    {
        ReviewConfiguration.Configure(_database);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}
