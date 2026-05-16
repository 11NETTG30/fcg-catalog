using FCG.Shared.Domain.Application;
using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
    public sealed class ObterJogoPorIdHandler
        : IRequestHandler<ObterJogoPorIdQuery, JogoPublicoResponse>
    {
        private readonly IJogoRepository _repository;
        private readonly ICacheService _cache;

        public ObterJogoPorIdHandler(IJogoRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<JogoPublicoResponse> Handle(
            ObterJogoPorIdQuery query,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"jogos:{query.Id}:publico";

            var cached = await _cache.GetAsync<JogoPublicoResponse>(cacheKey, cancellationToken);
            if (cached is not null)
                return cached;

            var jogo = await _repository.ObterPorId(query.Id, cancellationToken);

            if (jogo is null || !jogo.Ativo)
                throw new NotFoundException($"O jogo de id {query.Id} não foi encontrado.");

            var response = new JogoPublicoResponse(
                Id: jogo.Id,
                Titulo: jogo.Titulo,
                Descricao: jogo.Descricao,
                Preco: jogo.Preco.Valor,
                DataLancamento: jogo.DataLancamento
            );

            await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(10), cancellationToken);

            return response;
        }
    }
}
