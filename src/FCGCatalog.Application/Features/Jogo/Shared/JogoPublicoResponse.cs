namespace FCGCatalog.Application.Features.Jogo.Shared
{
	public record JogoPublicoResponse(
		Guid Id,
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento
	);
}
