using MediatR;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed record CriarJogoCommand
(
	string Titulo,
	string Descricao,
	decimal Preco,
	DateTime DataLancamento
) : IRequest<CriarJogoResponse>;
