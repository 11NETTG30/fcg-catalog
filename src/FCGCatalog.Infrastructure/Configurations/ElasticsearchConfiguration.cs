using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FCGCatalog.Application.Abstractions.Search;
using FCGCatalog.Infrastructure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCGCatalog.Infrastructure.Configurations
{
	public static class ElasticsearchConfiguration
	{
		public static IServiceCollection AddElasticsearch(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			var settings = configuration
				.GetSection("Elasticsearch")
				.Get<ElasticsearchSettings>()
				?? throw new InvalidOperationException("Elasticsearch settings não configuradas.");

			var clientSettings = !string.IsNullOrWhiteSpace(settings.CloudId)
				? new ElasticsearchClientSettings(settings.CloudId, new ApiKey(settings.ApiKey!))
				: new ElasticsearchClientSettings(new Uri(settings.Uri!));

			clientSettings.DefaultIndex(settings.IndiceJogos);

			services.AddSingleton(settings);
			services.AddSingleton(new ElasticsearchClient(clientSettings));
			services.AddSingleton<IIndiceJogoService, JogoIndiceService>();
			services.AddSingleton<IndiceJogosInicializador>();
			services.AddSingleton<IBuscaJogoService, BuscaJogoService>();

			return services;
		}
	}
}
