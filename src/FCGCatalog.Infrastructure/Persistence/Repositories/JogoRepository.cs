using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Uow;
using Microsoft.EntityFrameworkCore;

namespace FCGCatalog.Infrastructure.Persistence.Repositories;

public sealed class JogoRepository : IJogoRepository
{
	private readonly CatalogoDbContext _dbContext;
	private readonly DbSet<Jogo> _dbSet;
	public IUnitOfWork UnitOfWork => _dbContext;

	public JogoRepository(CatalogoDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbSet = _dbContext.Set<Jogo>();
	}

    public async Task Adicionar(Jogo jogo, CancellationToken cancellationToken)
    {
		await _dbSet.AddAsync(jogo, cancellationToken);
	}

	public async Task<bool> ExistePorTitulo(string titulo, CancellationToken cancellationToken)
	{
		string tituloNormalizado = titulo.Trim().ToLower();

		return await _dbSet.AnyAsync((j =>
			j.Titulo.Trim().ToLower() == tituloNormalizado), cancellationToken);
	}

	public async Task<Jogo?> ObterPorId(Guid id, CancellationToken cancellationToken)
	{
		return await _dbSet.FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
	}

}