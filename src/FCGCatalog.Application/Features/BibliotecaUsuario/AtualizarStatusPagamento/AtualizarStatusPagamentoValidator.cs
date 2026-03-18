using FluentValidation;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarStatusPagamento;

public sealed class AtualizarStatusPagamentoValidator : AbstractValidator<AtualizarStatusPagamentoCommand>
{
	public AtualizarStatusPagamentoValidator()
	{
		RuleFor(x => x.UsuarioId).NotEmpty();
		RuleFor(x => x.BibliotecaId).NotEmpty();
		RuleFor(x => x.StatusPagamento).IsInEnum();
	}
}
