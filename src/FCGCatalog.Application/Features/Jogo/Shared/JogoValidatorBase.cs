using FluentValidation;
using System.Linq.Expressions;

namespace FCGCatalog.Application.Features.Jogo.Shared
{
	public abstract class JogoValidatorBase<T> : AbstractValidator<T>
		where T : class
	{
		protected void AplicarRegrasJogo(
			Expression<Func<T, string>> titulo,
			Expression<Func<T, string?>> descricao,
			Expression<Func<T, decimal>> preco)
		{
			RuleFor(titulo)
				.NotEmpty()
				.MaximumLength(200);

			RuleFor(descricao)
				.MaximumLength(1000);

			RuleFor(preco)
				.GreaterThan(0)
				.PrecisionScale(18, 2, false)
				.WithMessage("Preço deve ser um valor positivo com no máximo 2 casas decimais e até 18 dígitos no total.");
		}
	}
}
