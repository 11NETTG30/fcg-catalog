using FCGCatalog.Application.Features.Jogo.Shared;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed class CriarJogoValidator : JogoValidatorBase<CriarJogoCommand>
{
	public CriarJogoValidator()
	{
		AplicarRegrasJogo(
			titulo: x => x.Titulo,
			descricao: x => x.Descricao,
			preco: x => x.Preco
		);
	}
}
