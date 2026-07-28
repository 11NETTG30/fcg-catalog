using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FCGCatalog.Application.Abstractions.Search;
using FCGCatalog.Application.Features.Jogo.Shared;
using Microsoft.Extensions.Logging;

namespace FCGCatalog.Infrastructure.Search;

internal sealed class BuscaJogoService : IBuscaJogoService
{
	private readonly ElasticsearchClient _client;
	private readonly ElasticsearchSettings _settings;
	private readonly ILogger<BuscaJogoService> _logger;

	public BuscaJogoService(
		ElasticsearchClient client,
		ElasticsearchSettings settings,
		ILogger<BuscaJogoService> logger)
	{
		_client = client;
		_settings = settings;
		_logger = logger;
	}

	public async Task<IEnumerable<JogoPublicoResponse>> BuscarAsync(
		string termo,
		int pagina,
		int tamanhoPagina,
		CancellationToken cancellationToken = default)
	{
		var from = (pagina - 1) * tamanhoPagina;

		var resposta = await _client.SearchAsync<JogoParaIndexar>(s => s
			.Index(_settings.IndiceJogos)
			.From(from)
			.Size(tamanhoPagina)
			.Query(q => q
				.Bool(b => b
					.Must(m => m
						.MultiMatch(mm => mm
							.Query(termo)
							.Fields(new[] { "titulo^3", "descricao" })
							.Fuzziness(new Fuzziness("AUTO"))
						)
					)
					.Filter(f => f
						.Term(t => t.Field("ativo").Value(true))
					)
				)
			),
			cancellationToken);

		if (!resposta.IsValidResponse)
		{
			_logger.LogError(
				"Falha ao buscar jogos no Elasticsearch: {Erro}", resposta.DebugInformation);
			throw new InvalidOperationException("Falha ao realizar busca no Elasticsearch.");
		}

		return resposta.Documents.Select(d => new JogoPublicoResponse(
			d.Id, d.Titulo, d.Descricao, d.Preco, d.DataLancamento));
	}
}