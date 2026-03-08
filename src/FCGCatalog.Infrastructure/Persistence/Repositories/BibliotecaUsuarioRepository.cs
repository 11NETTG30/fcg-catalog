using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Uow;

namespace FCGCatalog.Infrastructure.Persistence.Repositories;

public sealed class BibliotecaUsuarioRepository : IBibliotecaUsuarioRepository
{
	private readonly CatalogoDbContext _dbContext;
	public IUnitOfWork UnitOfWork => _dbContext;

	public BibliotecaUsuarioRepository(CatalogoDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public void Dispose()
	{
		_dbContext?.Dispose();
	}
}