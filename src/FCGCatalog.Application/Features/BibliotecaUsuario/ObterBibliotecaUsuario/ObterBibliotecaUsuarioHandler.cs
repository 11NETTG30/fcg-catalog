using FCG.Shared.Domain.Application;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed class ObterBibliotecaUsuarioHandler : IRequestHandler<ObterBibliotecaUsuarioQuery, IEnumerable<ObterBibliotecaUsuarioResponse>>
{
	private readonly IBibliotecaUsuarioRepository _biblioteca;
	private readonly ICacheService _cache;

	public ObterBibliotecaUsuarioHandler(IBibliotecaUsuarioRepository biblioteca, ICacheService cache)
	{
		_biblioteca = biblioteca;
		_cache = cache;
	}

	public async Task<IEnumerable<ObterBibliotecaUsuarioResponse>> Handle(
		ObterBibliotecaUsuarioQuery request,
		CancellationToken cancellationToken)
	{
		var cacheKey = $"biblioteca:{request.UsuarioId}";

		var cached = await _cache.GetAsync<IEnumerable<ObterBibliotecaUsuarioResponse>>(cacheKey, cancellationToken);
		if (cached is not null)
			return cached;

		var itens = await _biblioteca.ObterPorUsuarioId(request.UsuarioId, cancellationToken);

		var response = itens.Select(b => new ObterBibliotecaUsuarioResponse(
			b.JogoId,
			b.Jogo?.Titulo ?? string.Empty,
			b.DataCompra)).ToList();

		await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

		return response;
	}
}
