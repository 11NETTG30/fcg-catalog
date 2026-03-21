namespace FCGCatalog.API.Contracts.Jogo
{
	public sealed record CriarJogoRequest
	(
		string Titulo,
		string Descricao,
		decimal Preco,
		DateTime DataLancamento
	);
}
