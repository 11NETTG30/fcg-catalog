using FCGCatalog.Application.Features.Jogo.ObterJogoAdminPorId;
using FluentValidation;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
    public sealed class ObterJogoAdminPorIdValidator : AbstractValidator<ObterJogoAdminPorIdQuery>
	{
		public ObterJogoAdminPorIdValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}