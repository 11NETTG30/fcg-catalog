using FCGCatalog.Domain.Repositories;
using MediatR;
using BibliotecaUsuarioDomain = FCGCatalog.Domain.Entities.BibliotecaUsuario;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.LiberarJogoParaUsuario;

public sealed class LiberarJogoParaUsuarioHandler : IRequestHandler<LiberarJogoParaUsuarioCommand, Unit>
{
    private readonly IBibliotecaUsuarioRepository _repository;

    public LiberarJogoParaUsuarioHandler(IBibliotecaUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        LiberarJogoParaUsuarioCommand command,
        CancellationToken cancellationToken)
    {
        var item = BibliotecaUsuarioDomain.Criar(
            usuarioId: command.UsuarioId,
            jogoId: command.JogoId
        );

        await _repository.Adicionar(item, cancellationToken);
        await _repository.UnitOfWork.Commit(cancellationToken);

        return Unit.Value;
    }
}
