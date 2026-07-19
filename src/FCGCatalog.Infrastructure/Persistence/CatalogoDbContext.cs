using FCGCatalog.Domain.Entities;
using FCGCatalog.Infrastructure.Persistence.Configurations;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FCGCatalog.Infrastructure.Persistence;

public sealed class CatalogoDbContext : DbContextUoW
{
	public DbSet<Jogo> Jogos => Set<Jogo>();

	public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CatalogoDbContext).Assembly,
			type => type.Namespace == typeof(JogoConfiguration).Namespace
		);

		modelBuilder.AddInboxStateEntity();
		modelBuilder.AddOutboxMessageEntity();
		modelBuilder.AddOutboxStateEntity();
	}
}

