namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
    public sealed record ObterJogoPorIdResponse
    (  
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
