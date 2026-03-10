using MediatR;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed record CriarJogoRequest
(
	string Titulo,
	string Descricao,
	decimal Preco,
	DateTime DataLancamento
) : IRequest<CriarJogoResponse>;
