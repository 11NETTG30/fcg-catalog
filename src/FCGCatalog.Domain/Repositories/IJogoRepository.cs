using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.Repositories;

public interface IJogoRepository : IRepository<Jogo>
{
    Task Adicionar(Jogo jogo);
    Task<bool> ExistePorTitulo(string titulo);
}
