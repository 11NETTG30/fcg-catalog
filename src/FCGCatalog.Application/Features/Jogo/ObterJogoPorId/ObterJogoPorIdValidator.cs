using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FluentValidation;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
    public sealed class ObterJogoPorIdValidator : AbstractValidator<ObterJogoPorIdQuery>
	{
		public ObterJogoPorIdValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}