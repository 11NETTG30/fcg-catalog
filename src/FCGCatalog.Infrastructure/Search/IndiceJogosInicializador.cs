using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Microsoft.Extensions.Logging;

namespace FCGCatalog.Infrastructure.Search;

public sealed class IndiceJogosInicializador
{
	private readonly ElasticsearchClient _client;
	private readonly ElasticsearchSettings _settings;
	private readonly ILogger<IndiceJogosInicializador> _logger;

	public IndiceJogosInicializador(
		ElasticsearchClient client,
		ElasticsearchSettings settings,
		ILogger<IndiceJogosInicializador> logger)
	{
		_client = client;
		_settings = settings;
		_logger = logger;
	}

	public async Task GarantirIndiceCriadoAsync(CancellationToken cancellationToken = default)
	{
		var existe = await _client.Indices.ExistsAsync(_settings.IndiceJogos, cancellationToken);

		if (existe.Exists)
		{
			_logger.LogInformation("Índice '{Indice}' já existe.", _settings.IndiceJogos);
			return;
		}

		var resposta = await _client.Indices.CreateAsync(_settings.IndiceJogos, c => c
			.Mappings(m => m
				.Properties(new Properties
				{
			{ "id", new KeywordProperty() },
			{ "titulo", new TextProperty { Fields = new Properties { { "keyword", new KeywordProperty { IgnoreAbove = 256 } } } } },
			{ "descricao", new TextProperty { Fields = new Properties { { "keyword", new KeywordProperty { IgnoreAbove = 256 } } } } },
			{ "preco", new ScaledFloatNumberProperty { ScalingFactor = 100 } },
			{ "dataLancamento", new DateProperty() },
			{ "ativo", new BooleanProperty() },
			{ "dataCriacao", new DateProperty() },
			{ "dataAtualizacao", new DateProperty() },
				})
			), cancellationToken);

		if (!resposta.IsValidResponse)
		{
			_logger.LogError("Falha ao criar índice '{Indice}': {Erro}", _settings.IndiceJogos, resposta.DebugInformation);
			throw new InvalidOperationException($"Falha ao criar índice '{_settings.IndiceJogos}' no Elasticsearch.");
		}

		_logger.LogInformation("Índice '{Indice}' criado com mapping explícito.", _settings.IndiceJogos);
	}
}