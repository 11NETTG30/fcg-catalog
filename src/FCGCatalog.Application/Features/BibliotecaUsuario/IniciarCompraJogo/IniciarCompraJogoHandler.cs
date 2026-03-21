using FCG.Shared.Contracts.Events;
using FCGCatalog.Application.Abstractions.Messaging;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;
namespace FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;

public sealed class IniciarCompraJogoHandler : IRequestHandler<IniciarCompraJogoCommand, Unit>
{
	private readonly IJogoRepository _jogoRepository;
	private readonly IBibliotecaUsuarioRepository _bibliotecaUsuarioRepository;
	private readonly IEventPublisher _eventPublisher;

	public IniciarCompraJogoHandler(IJogoRepository jogoRepository, IBibliotecaUsuarioRepository bibliotecaUsuarioRepository, IEventPublisher eventPublisher)
	{
		_jogoRepository = jogoRepository;
		_bibliotecaUsuarioRepository = bibliotecaUsuarioRepository;
		_eventPublisher = eventPublisher;
	}

	public async Task<Unit> Handle(
		IniciarCompraJogoCommand command,
		CancellationToken cancellationToken)
	{
		var jogo = await _jogoRepository.ObterPorId(
			command.JogoId,
			cancellationToken);

		if (jogo is null)
			throw new ValidationException("Jogo não encontrado.");

		var usuarioJaPossuiJogo = await _bibliotecaUsuarioRepository.ExistePorUsuarioIdEJogoId(
			command.UsuarioId,
			command.JogoId,
			cancellationToken);

		if (usuarioJaPossuiJogo)
			throw new ValidationException("Usuário já possui esse jogo na biblioteca.");

		var evento = new OrderPlacedEvent
		(
			GameId: jogo.Id,
			UserId: command.UsuarioId,
			Price: jogo.Preco.Valor,
			Email: command.Email
		);

		await _eventPublisher.PublishAsync(evento, cancellationToken);

		return Unit.Value;
	}
}
