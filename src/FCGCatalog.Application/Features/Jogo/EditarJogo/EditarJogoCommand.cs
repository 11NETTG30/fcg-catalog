using MediatR;

namespace FCGCatalog.Application.Features.Jogo.EditarJogo
{
	public record EditarJogoCommand(
		Guid Id,
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento
	) : IRequest<Unit>;
}
