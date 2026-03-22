using FCGCatalog.Application.Features.Jogo.Shared;

namespace FCGCatalog.Application.Features.Jogo.EditarJogo
{
	public sealed class EditarJogoValidator : JogoValidatorBase<EditarJogoCommand>
	{
		public EditarJogoValidator()
		{
			AplicarRegrasJogo(
				titulo: x => x.Titulo,
				descricao: x => x.Descricao,
				preco: x => x.Preco
			);
		}
	}
}
