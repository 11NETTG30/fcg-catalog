using FCGCatalog.Application.Features.Jogo.ComprarJogo;
using MediatR;

namespace FCGCatalog.API.Contracts.Jogo;

public sealed record IniciarCompraJogoRequest
(
	Guid UsuarioId
) : IRequest<Unit>;