using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Abstractions.Messaging;
using FCGCatalog.Application.Abstractions.Messaging.Events;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.AtivarJogo
{
	public sealed class AtivarJogoHandler : IRequestHandler<AtivarJogoCommand, Unit>
	{
		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;
		private readonly IEventPublisher _eventPublisher;

		public AtivarJogoHandler(IJogoRepository repository, ICacheService cache, IEventPublisher eventPublisher)
		{
			_repository = repository;
			_cache = cache;
			_eventPublisher = eventPublisher;
		}

		public async Task<Unit> Handle(AtivarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken)
				?? throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			jogo.Ativar();

			_repository.Atualizar(jogo, cancellationToken);

			var evento = new JogoAlteradoEvent(
				jogo.Id, jogo.Titulo, jogo.Descricao, jogo.Preco.Valor,
				jogo.DataLancamento, jogo.Ativo, jogo.DataCriacao, jogo.DataAtualizacao);

			await _eventPublisher.PublishAsync(evento, cancellationToken);

			await _repository.UnitOfWork.Commit(cancellationToken);

			await _cache.RemoveByPrefixAsync("jogos:lista:", cancellationToken);

			return Unit.Value;
		}
	}
}