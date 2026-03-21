using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Uow;
using Microsoft.EntityFrameworkCore;

namespace FCGCatalog.Infrastructure.Persistence.Repositories;

public sealed class BibliotecaUsuarioRepository : IBibliotecaUsuarioRepository
{
	private readonly DbSet<BibliotecaUsuario> _dbSet;
	private readonly CatalogoDbContext _dbContext;
	public IUnitOfWork UnitOfWork => _dbContext;

	public BibliotecaUsuarioRepository(CatalogoDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbSet = _dbContext.Set<BibliotecaUsuario>();
	}

    public Task Adicionar(BibliotecaUsuario item, CancellationToken cancellationToken)
    {
		return _dbSet.AddAsync(item, cancellationToken).AsTask();
	}

	public Task<bool> ExistePorUsuarioIdEJogoId(Guid usuarioId, Guid jogoId, CancellationToken cancellationToken)
	{
		return _dbSet.AnyAsync(bu => bu.UsuarioId == usuarioId && bu.JogoId == jogoId, cancellationToken);
	}
}