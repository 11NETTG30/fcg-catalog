using MediatR;

namespace FCGCatalog.Application.Features.Review.CriarReview;

public sealed record CriarReviewCommand(
    Guid UsuarioId,    
    Guid JogoId,
    int Nota,
    string? Comentario
): IRequest<CriarReviewResponse>;