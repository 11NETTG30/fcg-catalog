using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Abstractions.Messaging;
using FCGCatalog.Application.Abstractions.Messaging.Events;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.ValueObjects;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.EditarJogo
{
	public sealed class EditarJogoHandler : IRequestHandler<EditarJogoCommand, Unit>
	{
		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;
		private readonly IEventPublisher _eventPublisher;

		public EditarJogoHandler(IJogoRepository repository, ICacheService cache, IEventPublisher eventPublisher)
		{
			_repository = repository;
			_cache = cache;
			_eventPublisher = eventPublisher;
		}

		public async Task<Unit> Handle(EditarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken)
				?? throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			var jogoJaExiste = await _repository.ExistePorTitulo(command.Titulo, cancellationToken);

			if (jogoJaExiste)
				throw new ConflictException("Já existe um jogo com esse título.");

			jogo.Editar(
				titulo: command.Titulo,
				descricao: command.Descricao,
				preco: Preco.Criar(command.Preco),
				dataLancamento: command.DataLancamento
			);

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