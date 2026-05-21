using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

namespace FCGCatalog.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoCollection<Review> _collection;

    public ReviewRepository(MongoDbContext context)
    {
        _collection = context.GetCollection<Review>("reviews");
    }

    public async Task Adicionar(Review review, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(review, cancellationToken: cancellationToken);
    }

    public async Task Atualizar(Review review, CancellationToken cancellationToken)
    {
        var filtro = Builders<Review>.Filter.Eq(r => r.Id, review.Id);
        await _collection.ReplaceOneAsync(filtro, review, cancellationToken: cancellationToken);
    }

    public async Task<bool> Deletar(Review review, CancellationToken cancellationToken)
    {
        var filtro = Builders<Review>.Filter.Eq(r => r.Id, review.Id);
        var result = await _collection.DeleteOneAsync(filtro, cancellationToken);

        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistePorUsuarioEJogo(Guid usuarioId, Guid jogoId, CancellationToken cancellationToken)
    {
        var filtro = Builders<Review>.Filter.And(
                        Builders<Review>.Filter.Eq(r => r.UsuarioId, usuarioId),
                        Builders<Review>.Filter.Eq(r => r.JogoId, jogoId)
        );

        return await _collection.Find(filtro).AnyAsync(cancellationToken);
    }

    public async Task<Review?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var filtro = Builders<Review>.Filter.Eq(r => r.Id, id);

        return await _collection
                    .Find(filtro)
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review>> ObterReviewsPorJogo(Guid jogoId, CancellationToken cancellationToken)
    {
        var filtro = Builders<Review>.Filter.Eq(r => r.JogoId, jogoId);

        return await _collection
                    .Find(filtro)
                    .SortByDescending(r => r.DataCriacao)
                    .ToListAsync(cancellationToken);
    }
}
