using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FCGCatalog.Infrastructure.Persistence
{
	public sealed class CatalogoDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CatalogoDbContext>
	{
		public CatalogoDbContext CreateDbContext(string[] args)
		{
			Env.TraversePath().Load();

			var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
				?? throw new InvalidOperationException(
					"ConnectionStrings__DefaultConnection não encontrada no .env.");

			connectionString = connectionString.Replace("Host=postgres;", "Host=localhost;");

			var optionsBuilder = new DbContextOptionsBuilder<CatalogoDbContext>();
			optionsBuilder.UseNpgsql(connectionString, o =>
				o.MigrationsHistoryTable("__ef_migrations_history"));

			return new CatalogoDbContext(optionsBuilder.Options);
		}
	}
}
