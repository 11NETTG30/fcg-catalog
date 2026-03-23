using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.LiberarJogoParaUsuario;

public sealed record LiberarJogoParaUsuarioCommand
(
    Guid UsuarioId,
    Guid JogoId
) : IRequest<Unit>;
