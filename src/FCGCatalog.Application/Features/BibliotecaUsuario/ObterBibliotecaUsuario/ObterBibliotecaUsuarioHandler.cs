using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed class ObterBibliotecaUsuarioHandler : IRequestHandler<ObterBibliotecaUsuarioQuery, ObterBibliotecaUsuarioResponse>
{
	private readonly IBibliotecaUsuarioRepository _biblioteca;

	public ObterBibliotecaUsuarioHandler(IBibliotecaUsuarioRepository biblioteca)
	{
		_biblioteca = biblioteca;
	}

	public async Task<ObterBibliotecaUsuarioResponse> Handle(
		ObterBibliotecaUsuarioQuery request,
		CancellationToken cancellationToken)
	{
		// Implementação simplificada - você pode melhorar com queries mais sofisticadas
		// Para este exemplo, retornamos um erro se o usuário não for encontrado
		throw new NotImplementedException("Método de obtenção de biblioteca do usuário não implementado no repositório");
	}
}
