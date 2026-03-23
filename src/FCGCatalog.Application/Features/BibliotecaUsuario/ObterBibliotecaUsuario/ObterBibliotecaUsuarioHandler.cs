using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed class ObterBibliotecaUsuarioHandler : IRequestHandler<ObterBibliotecaUsuarioQuery, IEnumerable<ObterBibliotecaUsuarioResponse>>
{
	private readonly IBibliotecaUsuarioRepository _biblioteca;

	public ObterBibliotecaUsuarioHandler(IBibliotecaUsuarioRepository biblioteca)
	{
		_biblioteca = biblioteca;
	}

	public async Task<IEnumerable<ObterBibliotecaUsuarioResponse>> Handle(
		ObterBibliotecaUsuarioQuery request,
		CancellationToken cancellationToken)
	{
		var itens = await _biblioteca.ObterPorUsuarioId(request.UsuarioId, cancellationToken);

		return itens.Select(b => new ObterBibliotecaUsuarioResponse(
			b.JogoId,
			b.Jogo?.Titulo ?? string.Empty,
			b.DataCompra));
	}
}
