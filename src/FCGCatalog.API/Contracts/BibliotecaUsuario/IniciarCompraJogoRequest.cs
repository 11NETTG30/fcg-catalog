using MediatR;

namespace FCGCatalog.API.Contracts.BibliotecaUsuario;

public sealed record IniciarCompraJogoRequest
(
	Guid JogoId
) : IRequest<Unit>;