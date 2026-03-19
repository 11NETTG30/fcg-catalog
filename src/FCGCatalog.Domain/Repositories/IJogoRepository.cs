using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.Repositories;

public interface IJogoRepository : IRepository<Jogo>
{
    Task Adicionar(Jogo jogo, CancellationToken cancellationToken);
	Task<bool> ExistePorTitulo(string titulo, CancellationToken cancellationToken);
	Task<Jogo?> ObterPorId(Guid id, CancellationToken cancellationToken);
}
