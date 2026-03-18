using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarBibliotecaUsuario;

public sealed class AtualizarBibliotecaUsuarioHandler : IRequestHandler<AtualizarBibliotecaUsuarioCommand, Unit>
{
	private readonly IBibliotecaUsuarioRepository _biblioteca;
	private readonly IJogoRepository _jogoRepository;

	public AtualizarBibliotecaUsuarioHandler(
		IBibliotecaUsuarioRepository biblioteca,
		IJogoRepository jogoRepository)
	{
		_biblioteca = biblioteca;
		_jogoRepository = jogoRepository;
	}

	public async Task<Unit> Handle(
		AtualizarBibliotecaUsuarioCommand request,
		CancellationToken cancellationToken)
	{
		var jogo = await _jogoRepository.ObterPorId(request.JogoId, cancellationToken);

		if (jogo is null)
			throw new ValidationException("Jogo não encontrado.");

		// Implementação simplificada - você pode melhorar com métodos de atualização no repositório
		throw new NotImplementedException("Método de atualização de biblioteca do usuário não implementado no repositório");
	}
}
