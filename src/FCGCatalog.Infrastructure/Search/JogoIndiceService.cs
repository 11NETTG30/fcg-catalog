using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Logging;

namespace FCGCatalog.Infrastructure.Search
{
	public sealed class JogoIndiceService : IIndiceJogoService
	{
		private readonly ElasticsearchClient _client;
		private readonly ElasticsearchSettings _settings;
		private readonly ILogger<JogoIndiceService> _logger;

		public JogoIndiceService(
			ElasticsearchClient client,
			ElasticsearchSettings settings,
			ILogger<JogoIndiceService> logger)
		{
			_client = client;
			_settings = settings;
			_logger = logger;
		}

		public async Task IndexarAsync(JogoParaIndexar jogo, CancellationToken cancellationToken = default)
		{
			var resposta = await _client.IndexAsync(
				jogo,
				idx => idx.Index(_settings.IndiceJogos).Id(jogo.Id),
				cancellationToken);

			if (!resposta.IsValidResponse)
			{
				_logger.LogError(
					"Falha ao indexar jogo {JogoId} no Elasticsearch: {Erro}",
					jogo.Id, resposta.DebugInformation);

				throw new InvalidOperationException($"Falha ao indexar jogo {jogo.Id} no Elasticsearch.");
			}
		}
	}
}
