using FCG.Shared.Domain.Application;
using FCGCatalog.Domain.Repositories;
using MediatR;
using BibliotecaUsuarioDomain = FCGCatalog.Domain.Entities.BibliotecaUsuario;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.LiberarJogoParaUsuario;

public sealed class LiberarJogoParaUsuarioHandler : IRequestHandler<LiberarJogoParaUsuarioCommand, Unit>
{
    private readonly IBibliotecaUsuarioRepository _repository;
    private readonly ICacheService _cache;

    public LiberarJogoParaUsuarioHandler(IBibliotecaUsuarioRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
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

        await _cache.RemoveAsync($"biblioteca:{command.UsuarioId}", cancellationToken);

        return Unit.Value;
    }
}
