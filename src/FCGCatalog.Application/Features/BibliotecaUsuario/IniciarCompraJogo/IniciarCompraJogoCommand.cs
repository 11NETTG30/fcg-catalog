using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;

public sealed record IniciarCompraJogoCommand
(
	Guid UsuarioId,
	Guid JogoId,
	string Email
) : IRequest<Unit>;
