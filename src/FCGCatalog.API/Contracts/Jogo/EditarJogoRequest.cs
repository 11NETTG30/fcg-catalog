namespace FCGCatalog.API.Contracts.Jogo
{
	public record EditarJogoRequest(
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento
	);
}
