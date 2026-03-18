using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarBibliotecaUsuario;

public sealed record AtualizarBibliotecaUsuarioCommand(
	Guid UsuarioId,
	Guid Id,
	Guid JogoId) : IRequest<Unit>;
