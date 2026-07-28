namespace FCGCatalog.Infrastructure.Search
{
	public sealed class ElasticsearchSettings
	{
		public string? Uri { get; set; }       
		public string? CloudId { get; set; }   
		public string? ApiKey { get; set; }    
		public string IndiceJogos { get; set; } = "jogos";
	}
}
