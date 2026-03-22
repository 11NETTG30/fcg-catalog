using FluentValidation;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;

public sealed class IniciarCompraJogoValidator : AbstractValidator<IniciarCompraJogoCommand>
{
	public IniciarCompraJogoValidator()
	{
		RuleFor(x => x.JogoId)
			.NotEmpty();

		RuleFor(x => x.UsuarioId)
			.NotEmpty();

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}
