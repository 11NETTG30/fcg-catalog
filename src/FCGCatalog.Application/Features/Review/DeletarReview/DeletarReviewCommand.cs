using MediatR;

namespace FCGCatalog.Application.Features.Review.DeletarReview;

public sealed record DeletarReviewCommand(
    Guid Id,
    Guid UsuarioId
) : IRequest<Unit>;
