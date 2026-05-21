using MediatR;

namespace FCGCatalog.Application.Features.Review.EditarReview;

public sealed record EditarReviewCommand(
    Guid Id,
    Guid UsuarioId,
    int Nota,
    string? Comentario
) : IRequest<Unit>;