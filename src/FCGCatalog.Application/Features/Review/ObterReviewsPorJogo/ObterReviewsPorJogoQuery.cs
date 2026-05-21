using FCGCatalog.Application.Features.Review.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Review.ObterReviewsPorJogo;

public sealed record ObterReviewsPorJogoQuery(
    Guid Id
) : IRequest<IEnumerable<ReviewResponse>>;
