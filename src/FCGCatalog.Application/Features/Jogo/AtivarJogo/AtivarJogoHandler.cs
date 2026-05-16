using FCG.Shared.Domain.Application;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.AtivarJogo
{
	public sealed class AtivarJogoHandler : IRequestHandler<AtivarJogoCommand, Unit>
	{
		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;

		public AtivarJogoHandler(IJogoRepository repository, ICacheService cache)
		{
			_repository = repository;
			_cache = cache;
		}

		public async Task<Unit> Handle(AtivarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			jogo.Ativar();

			_repository.Atualizar(jogo, cancellationToken);
			await _repository.UnitOfWork.Commit(cancellationToken);

			await Task.WhenAll(
				_cache.RemoveAsync($"jogos:{command.Id}:publico", cancellationToken),
				_cache.RemoveAsync($"jogos:{command.Id}:admin", cancellationToken),
				_cache.RemoveByPrefixAsync("jogos:lista:", cancellationToken)
			);

			return Unit.Value;
		}
	}
}
