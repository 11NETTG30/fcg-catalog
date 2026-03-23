using FluentValidation;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.LiberarJogoParaUsuario;

public sealed class LiberarJogoParaUsuarioValidator : AbstractValidator<LiberarJogoParaUsuarioCommand>
{
    public LiberarJogoParaUsuarioValidator()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty();

        RuleFor(x => x.JogoId)
            .NotEmpty();
    }
}
