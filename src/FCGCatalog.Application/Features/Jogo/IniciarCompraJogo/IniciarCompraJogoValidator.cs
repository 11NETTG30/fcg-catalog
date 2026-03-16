using FluentValidation;

namespace FCGCatalog.Application.Features.Jogo.ComprarJogo;

public sealed class IniciarCompraJogoValidator : AbstractValidator<IniciarCompraJogoCommand>
{
	public IniciarCompraJogoValidator()
	{
		RuleFor(x => x.JogoId)
			.NotEmpty();

		RuleFor(x => x.UsuarioId)
			.NotEmpty();
	}
}
