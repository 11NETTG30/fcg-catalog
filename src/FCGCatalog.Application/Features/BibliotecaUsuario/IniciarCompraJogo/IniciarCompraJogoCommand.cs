using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;

public sealed record IniciarCompraJogoCommand
(
		Guid JogoId,
		Guid UsuarioId
) : IRequest<Unit>;
