using FCGCatalog.Application.Abstractions.Search;
using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.BuscarJogos;

public sealed class BuscarJogosHandler : IRequestHandler<BuscarJogosQuery, IEnumerable<JogoPublicoResponse>>
{
	private readonly IBuscaJogoService _buscaJogoService;

	public BuscarJogosHandler(IBuscaJogoService buscaJogoService)
	{
		_buscaJogoService = buscaJogoService;
	}

	public Task<IEnumerable<JogoPublicoResponse>> Handle(BuscarJogosQuery query, CancellationToken cancellationToken)
		=> _buscaJogoService.BuscarAsync(query.Termo, query.Pagina, query.TamanhoPagina, cancellationToken);
}