using FCGCatalog.Domain.Shared.Uow;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FCGCatalog.Infrastructure.Persistence;

public abstract class DbContextUoW : DbContext, IUnitOfWork
{
	protected DbContextUoW(DbContextOptions options) : base(options)
	{

	}

	public async Task<bool> Commit(CancellationToken cancellationToken)
	{
		return await SaveChangesAsync(cancellationToken) > 0;
	}
}
