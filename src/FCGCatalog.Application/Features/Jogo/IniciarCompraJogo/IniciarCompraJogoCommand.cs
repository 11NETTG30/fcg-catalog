using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ComprarJogo;

public sealed record IniciarCompraJogoCommand
(
		Guid JogoId,
		Guid UsuarioId
) : IRequest<Unit>;
