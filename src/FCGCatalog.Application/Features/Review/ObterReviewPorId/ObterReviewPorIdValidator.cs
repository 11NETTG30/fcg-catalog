using FluentValidation;

namespace FCGCatalog.Application.Features.Review.ObterReviewPorId;

public sealed class ObterReviewPorIdValidator : AbstractValidator<ObterReviewPorIdQuery>
{
    public ObterReviewPorIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}