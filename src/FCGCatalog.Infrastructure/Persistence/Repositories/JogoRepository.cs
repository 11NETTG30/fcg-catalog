using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Uow;

namespace FCGCatalog.Infrastructure.Persistence.Repositories;

public sealed class JogoRepository : IJogoRepository
{
	private readonly CatalogoDbContext _dbContext;
	public IUnitOfWork UnitOfWork => _dbContext;

	public JogoRepository(CatalogoDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public void Dispose()
	{
		_dbContext?.Dispose();
	}

    public Task Adicionar(Jogo jogo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistePorTitulo(string titulo)
    {
        throw new NotImplementedException();
    }
}