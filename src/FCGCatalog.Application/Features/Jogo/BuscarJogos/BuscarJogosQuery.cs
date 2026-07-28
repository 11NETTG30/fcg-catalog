using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.BuscarJogos;

public sealed record BuscarJogosQuery(
	string Termo,
	int Pagina = 1,
	int TamanhoPagina = 10) : IRequest<IEnumerable<JogoPublicoResponse>>;