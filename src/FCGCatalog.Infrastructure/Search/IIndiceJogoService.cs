namespace FCGCatalog.Infrastructure.Search
{
	public interface IIndiceJogoService
	{
		Task IndexarAsync(JogoParaIndexar jogo, CancellationToken cancellationToken = default);
	}
}
