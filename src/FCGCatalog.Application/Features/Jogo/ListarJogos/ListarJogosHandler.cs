using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogos
{
	public sealed class ListarJogosHandler : IRequestHandler<ListarJogosQuery, IEnumerable<JogoAdminResponse>>
	{
		private const string CacheKey = "jogos:lista:admin";

		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;

		public ListarJogosHandler(IJogoRepository repository, ICacheService cache)
		{
			_repository = repository;
			_cache = cache;
		}

		public async Task<IEnumerable<JogoAdminResponse>> Handle(
			ListarJogosQuery request,
			CancellationToken cancellationToken)
		{
			var cached = await _cache.GetAsync<IEnumerable<JogoAdminResponse>>(CacheKey, cancellationToken);
			if (cached is not null)
				return cached;

			var jogos = await _repository.ObterJogos(somenteAtivos: false, cancellationToken);

			var response = jogos.Select(j => new JogoAdminResponse(
				Id: j.Id,
				Titulo: j.Titulo,
				Descricao: j.Descricao,
				Preco: j.Preco.Valor,
				DataLancamento: j.DataLancamento,
				Ativo: j.Ativo,
				DataCriacao: j.DataCriacao,
				DataAtualizacao: j.DataAtualizacao
			)).ToList();

			await _cache.SetAsync(CacheKey, response, TimeSpan.FromMinutes(5), cancellationToken);

			return response;
		}
	}
}
