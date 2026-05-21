using FCGCatalog.Application.Features.Review.Shared;
using FluentValidation;

namespace FCGCatalog.Application.Features.Review.CriarReview;

public sealed class CriarReviewValidator : ReviewValidatorBase<CriarReviewCommand>
{
    public CriarReviewValidator()
    {
        AplicarRegrasReview(
            usuarioId: x => x.UsuarioId,
            nota: x => x.Nota,
            comentario: x => x.Comentario
        );

        RuleFor(x => x.JogoId)
                .NotEmpty();
    }
}
