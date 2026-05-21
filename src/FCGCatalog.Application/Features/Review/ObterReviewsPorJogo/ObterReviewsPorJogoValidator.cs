using FluentValidation;

namespace FCGCatalog.Application.Features.Review.ObterReviewsPorJogo;

public sealed class ObterReviewsPorJogoValidator : AbstractValidator<ObterReviewsPorJogoQuery>
{
    public ObterReviewsPorJogoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}