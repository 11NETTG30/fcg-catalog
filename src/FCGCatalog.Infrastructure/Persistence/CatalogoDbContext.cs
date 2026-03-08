using Microsoft.EntityFrameworkCore;

namespace FCGCatalog.Infrastructure.Persistence;

public sealed class CatalogoDbContext : DbContextUoW
{
	public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

	}

}

