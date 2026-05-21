using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.Repositories;

public interface IReviewRepository : IDocumentRepository<Review>
{
    Task Adicionar(Review review, CancellationToken cancellationToken);
    Task<Review?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Review>> ObterReviewsPorJogo(Guid jogoId, CancellationToken cancellationToken);
    Task<bool> ExistePorUsuarioEJogo(Guid usuarioId, Guid jogoId, CancellationToken cancellationToken);
    Task Atualizar(Review review, CancellationToken cancellationToken);
    Task<bool> Deletar(Review review, CancellationToken cancellationToken);
}
