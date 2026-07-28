namespace FCGCatalog.Application.Abstractions.Messaging.Events
{

	public sealed record JogoAlteradoEvent(
		Guid Id,
		string Titulo,
		string? Descricao,
		decimal Preco,
		DateTime? DataLancamento,
		bool Ativo,
		DateTime DataCriacao,
		DateTime? DataAtualizacao)
		: JogoEventBase(Id, Titulo, Descricao, Preco, DataLancamento, Ativo, DataCriacao, DataAtualizacao);
}
