using FluentValidation;
using System.Linq.Expressions;

namespace FCGCatalog.Application.Features.Review.Shared
{
	public abstract class ReviewValidatorBase<T> : AbstractValidator<T>
		where T : class
	{
		protected void AplicarRegrasReview(
			Expression<Func<T, Guid>> usuarioId,
			Expression<Func<T, int>> nota,
            Expression<Func<T, string?>> comentario)
		{
            RuleFor(usuarioId)
                .NotEmpty();

            RuleFor(nota)
                .InclusiveBetween(1, 5)
                .WithMessage("A nota deve estar entre 1 e 5.");

            RuleFor(comentario)
                .MaximumLength(1000)
                .WithMessage("O comentário não pode exceder 1000 caracteres.");
        }
	}
}