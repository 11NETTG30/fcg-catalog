namespace FCGCatalog.Application.Features.Jogo.Shared
{
	public record JogoAdminResponse(
		Guid Id,
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento,
		bool Ativo,
		DateTime DataCriacao,
		DateTime? DataAtualizacao
	);
}
