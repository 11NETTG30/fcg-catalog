using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoAdminPorId
{
	public sealed class ObterJogoAdminPorIdHandler
		: IRequestHandler<ObterJogoAdminPorIdQuery, JogoAdminResponse>
	{
		private readonly IJogoRepository _repository;
		private readonly ICacheService _cache;

		public ObterJogoAdminPorIdHandler(IJogoRepository repository, ICacheService cache)
		{
			_repository = repository;
			_cache = cache;
		}

		public async Task<JogoAdminResponse> Handle(
			ObterJogoAdminPorIdQuery query,
			CancellationToken cancellationToken)
		{
			var cacheKey = $"jogos:{query.Id}:admin";

			var cached = await _cache.GetAsync<JogoAdminResponse>(cacheKey, cancellationToken);
			if (cached is not null)
				return cached;

			var jogo = await _repository.ObterPorId(query.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {query.Id} não foi encontrado.");

			var response = new JogoAdminResponse(
				Id: jogo.Id,
				Titulo: jogo.Titulo,
				Descricao: jogo.Descricao,
				Preco: jogo.Preco.Valor,
				DataLancamento: jogo.DataLancamento,
				Ativo: jogo.Ativo,
				DataCriacao: jogo.DataCriacao,
				DataAtualizacao: jogo.DataAtualizacao
			);

			await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(10), cancellationToken);

			return response;
		}
	}
}
