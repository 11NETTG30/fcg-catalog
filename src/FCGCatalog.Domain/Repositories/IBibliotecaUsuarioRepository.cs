using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.Repositories;

public interface IBibliotecaUsuarioRepository : IRepository<BibliotecaUsuario>
{
	Task Adicionar(BibliotecaUsuario item, CancellationToken cancellationToken);
}
