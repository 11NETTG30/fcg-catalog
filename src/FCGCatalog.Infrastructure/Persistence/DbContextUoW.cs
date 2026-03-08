using FCGCatalog.Domain.Shared.Uow;
using Microsoft.EntityFrameworkCore;

namespace FCGCatalog.Infrastructure.Persistence;

public abstract class DbContextUoW : DbContext, IUnitOfWork
{
	protected DbContextUoW(DbContextOptions options) : base(options)
	{

	}

	public async Task<bool> Commit()
	{
		return await SaveChangesAsync() > 0;
	}
}
