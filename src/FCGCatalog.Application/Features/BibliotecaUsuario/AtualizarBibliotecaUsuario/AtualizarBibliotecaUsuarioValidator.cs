using FluentValidation;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarBibliotecaUsuario;

public sealed class AtualizarBibliotecaUsuarioValidator : AbstractValidator<AtualizarBibliotecaUsuarioCommand>
{
	public AtualizarBibliotecaUsuarioValidator()
	{
		RuleFor(x => x.UsuarioId).NotEmpty();
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.JogoId).NotEmpty();
	}
}
