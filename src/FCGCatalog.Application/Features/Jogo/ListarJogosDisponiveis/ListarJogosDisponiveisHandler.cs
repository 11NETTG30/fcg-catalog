using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogosDisponiveis
{
	public sealed class ListarJogosDisponiveisHandler : IRequestHandler<ListarJogosDisponiveisQuery, IEnumerable<JogoPublicoResponse>>
	{
		private const string CacheKey = "jogos:lista:publico";

		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;

		public ListarJogosDisponiveisHandler(IJogoRepository repository, ICacheService cache)
		{
			_repository = repository;
			_cache = cache;
		}

		public async Task<IEnumerable<JogoPublicoResponse>> Handle(
			ListarJogosDisponiveisQuery request,
			CancellationToken cancellationToken)
		{
			var cached = await _cache.GetAsync<IEnumerable<JogoPublicoResponse>>(CacheKey, cancellationToken);
			if (cached is not null)
				return cached;

			var jogos = await _repository.ObterJogos(somenteAtivos: true, cancellationToken);

			var response = jogos.Select(j => new JogoPublicoResponse(
				Id: j.Id,
				Titulo: j.Titulo,
				Descricao: j.Descricao,
				Preco: j.Preco.Valor,
				DataLancamento: j.DataLancamento
			)).ToList();

			await _cache.SetAsync(CacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

			return response;
		}
	}
}
