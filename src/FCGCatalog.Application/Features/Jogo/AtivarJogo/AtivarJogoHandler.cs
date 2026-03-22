using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.Shared.Uow;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.AtivarJogo
{
	public sealed class AtivarJogoHandler : IRequestHandler<AtivarJogoCommand, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJogoRepository _repository;

		public AtivarJogoHandler(IUnitOfWork unitOfWork, IJogoRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public async Task<Unit> Handle(AtivarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			jogo.Ativar();

			_repository.Atualizar(jogo, cancellationToken);
			await _unitOfWork.Commit(cancellationToken);

			return Unit.Value;
		}
	}
}
