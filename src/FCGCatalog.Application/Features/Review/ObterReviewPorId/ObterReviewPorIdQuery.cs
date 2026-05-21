using FCGCatalog.Application.Features.Review.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Review.ObterReviewPorId;

public sealed record ObterReviewPorIdQuery(
    Guid Id
) : IRequest<ReviewResponse>;
