using FluentValidation;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed class CriarJogoValidator : AbstractValidator<CriarJogoRequest>
{
	public CriarJogoValidator()
	{
		RuleFor(x => x.Titulo)
			.NotEmpty()
			.MaximumLength(200);

		RuleFor(x => x.Descricao)
			.MaximumLength(1000);

		RuleFor(x => x.Preco)
			.GreaterThan(0)
			.PrecisionScale(18, 2, false)
			.WithMessage("Preço deve ser um valor positivo com no máximo 2 casas decimais e até 18 dígitos no total.");
	}
}
