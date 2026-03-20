using FCGCatalog.Application.Abstractions.Messaging;
using FCG.Shared.Contracts.Events;
using FCGCatalog.Domain.Repositories;
using MediatR;
using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;

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
			throw new ValidationException("Jogo não encontrado.");

		var evento = new OrderPlacedEvent
		(
			GameId: jogo.Id,
			UserId: request.UsuarioId,
			Price: jogo.Preco.Valor,
			Email: request.Email
		);

		await _eventPublisher.PublishAsync(evento, cancellationToken);

		return Unit.Value;
	}
}
