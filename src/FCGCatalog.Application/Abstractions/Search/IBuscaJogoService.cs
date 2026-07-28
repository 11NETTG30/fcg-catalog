using FCGCatalog.Application.Features.Jogo.Shared;

namespace FCGCatalog.Application.Abstractions.Search;

public interface IBuscaJogoService
{
	Task<IEnumerable<JogoPublicoResponse>> BuscarAsync(
		string termo,
		int pagina,
		int tamanhoPagina,
		CancellationToken cancellationToken = default);
}