using FCGCatalog.Application.Abstractions.Messaging;
using FCG.Contracts.Events;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ComprarJogo;

public sealed class IniciarCompraJogoHandler : IRequestHandler<IniciarCompraJogoCommand, Unit>
{
	private readonly IJogoRepository _jogoRepository;
	private readonly IEventPublisher _eventPublisher;

    public IniciarCompraJogoHandler(IJogoRepository jogoRepository, IEventPublisher eventPublisher)
    {
        _jogoRepository = jogoRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Unit> Handle(
		IniciarCompraJogoCommand request,
		CancellationToken cancellationToken)
	{
		var jogo = await _jogoRepository.ObterPorId(
			request.JogoId,
			cancellationToken);

		if (jogo is null)
			throw new InvalidOperationException("Jogo não encontrado.");

		var evento = new OrderPlacedEvent
		(
			GameId: jogo.Id,
			UserId: request.UsuarioId,
			Price: jogo.Preco.Valor
		);

		await _eventPublisher.PublishAsync(evento, cancellationToken);

		return Unit.Value;
	}
}
