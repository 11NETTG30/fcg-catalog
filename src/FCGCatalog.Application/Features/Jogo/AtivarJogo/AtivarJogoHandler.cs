using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.AtivarJogo
{
	public sealed class AtivarJogoHandler : IRequestHandler<AtivarJogoCommand, Unit>
	{
		private readonly IJogoRepository _repository;

        public AtivarJogoHandler(IJogoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AtivarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			jogo.Ativar();

			_repository.Atualizar(jogo, cancellationToken);
			await _repository.UnitOfWork.Commit(cancellationToken);

			return Unit.Value;
		}
	}
}
