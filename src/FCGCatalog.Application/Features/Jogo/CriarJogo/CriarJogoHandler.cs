using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Abstractions.Messaging;
using FCGCatalog.Application.Abstractions.Messaging.Events;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.ValueObjects;
using MediatR;
using JogoDomain = FCGCatalog.Domain.Entities.Jogo;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed class CriarJogoHandler : IRequestHandler<CriarJogoCommand, CriarJogoResponse>
{
	private readonly IJogoRepository _repository;
	private readonly ICacheService _cache;
	private readonly IEventPublisher _eventPublisher;

	public CriarJogoHandler(IJogoRepository repository, ICacheService cache, IEventPublisher eventPublisher)
	{
		_repository = repository;
		_cache = cache;
		_eventPublisher = eventPublisher;
	}

	public async Task<CriarJogoResponse> Handle(
		CriarJogoCommand command,
		CancellationToken cancellationToken)
	{
		var jogoJaExiste = await _repository.ExistePorTitulo(command.Titulo, cancellationToken);

		if (jogoJaExiste)
			throw new ConflictException("Já existe um jogo com esse título.");

		var jogo = JogoDomain.Criar(
			titulo: command.Titulo,
			descricao: command.Descricao,
			preco: Preco.Criar(command.Preco),
			dataLancamento: command.DataLancamento
		);

		await _repository.Adicionar(jogo, cancellationToken);

		var evento = new JogoCriadoEvent(
			jogo.Id, jogo.Titulo, jogo.Descricao, jogo.Preco.Valor,
			jogo.DataLancamento, jogo.Ativo, jogo.DataCriacao, jogo.DataAtualizacao);

		await _eventPublisher.PublishAsync(evento, cancellationToken);

		await _repository.UnitOfWork.Commit(cancellationToken);

		await _cache.RemoveByPrefixAsync("jogos:lista:", cancellationToken);

		return new CriarJogoResponse(jogo.Id);
	}
}