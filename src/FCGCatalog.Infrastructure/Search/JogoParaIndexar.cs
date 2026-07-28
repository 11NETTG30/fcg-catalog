namespace FCGCatalog.Infrastructure.Search
{
	public sealed record JogoParaIndexar(
		Guid Id,
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento,
		bool Ativo,
		DateTime DataCriacao,
		DateTime? DataAtualizacao);
}
