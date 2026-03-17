using MediatR;

namespace FCGCatalog.API.Contracts.Jogo;

public sealed record IniciarCompraJogoRequest
(
	Guid JogoId
) : IRequest<Unit>;