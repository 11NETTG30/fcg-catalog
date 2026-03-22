using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.Shared.Uow;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.InativarJogo
{
	public sealed class DesativarJogoHandler : IRequestHandler<DesativarJogoCommand, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJogoRepository _repository;

		public DesativarJogoHandler(IUnitOfWork unitOfWork, IJogoRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public async Task<Unit> Handle(DesativarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			jogo.Desativar();

			_repository.Atualizar(jogo, cancellationToken);
			await _unitOfWork.Commit(cancellationToken);

			return Unit.Value;
		}
	}
}
