using FCGCatalog.Application.Features.Review.Shared;

namespace FCGCatalog.Application.Features.Review.EditarReview;

public sealed class EditarReviewValidator : ReviewValidatorBase<EditarReviewCommand>
{
    public EditarReviewValidator()
    {
        AplicarRegrasReview(
            usuarioId: x => x.UsuarioId,
            nota: x => x.Nota,
            comentario: x => x.Comentario
        );
    }
}