using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.Entities;

public sealed class BibliotecaUsuario : Entity, IAggregateRoot
{
	public Guid UsuarioId { get; private set; }
	public Guid JogoId { get; private set; }
	public DateTime DataCompra { get; private set; }

	// navegação EF Core
	public Jogo? Jogo { get; private set; }

	public BibliotecaUsuario
	(
		Guid usuarioId,
		Guid jogoId
	)
	{
		if (usuarioId == Guid.Empty)
			throw new ValidationException("Usuário inválido");

		if (jogoId == Guid.Empty)
			throw new ValidationException("Jogo inválido");

		UsuarioId = usuarioId;
		JogoId = jogoId;
		DataCompra = DateTime.UtcNow;
	}

	// EF Core
	private BibliotecaUsuario() { }
}