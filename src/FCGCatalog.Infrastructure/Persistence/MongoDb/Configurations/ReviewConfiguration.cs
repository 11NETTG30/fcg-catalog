using FCG.Shared.Domain.Abstractions;
using FCGCatalog.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FCGCatalog.Infrastructure.Persistence.MongoDb.Configurations;

public static class ReviewConfiguration
{
    public static void Configure(IMongoDatabase database)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.MapIdMember(e => e.Id);

                map.SetIsRootClass(true);
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Review)))
        {
            BsonClassMap.RegisterClassMap<Review>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);

                map.MapMember(r => r.UsuarioId).SetElementName("usuarioId");
                map.MapMember(r => r.JogoId).SetElementName("jogoId");
                map.MapMember(r => r.Nota).SetElementName("nota");
                map.MapMember(r => r.Comentario).SetElementName("comentario");
                map.MapMember(r => r.DataCriacao).SetElementName("dataCriacao");

                map.MapCreator(_ =>
                    (Review)Activator.CreateInstance(
                        typeof(Review),
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                        binder: null,
                        args: null,
                        culture: null)!);
            });

            // Índices:
            var collection = database.GetCollection<Review>("reviews");

            var indices = new List<CreateIndexModel<Review>>
            {
                new(Builders<Review>.IndexKeys.Ascending(r => r.JogoId)),

                new(Builders<Review>.IndexKeys
                        .Ascending(r => r.UsuarioId)
                        .Ascending(r => r.JogoId),
                    new CreateIndexOptions { Unique = true })
            };

            collection.Indexes.CreateMany(indices);
        }
    }
}
