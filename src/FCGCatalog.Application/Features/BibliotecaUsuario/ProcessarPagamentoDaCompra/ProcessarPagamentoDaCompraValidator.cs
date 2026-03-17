using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ProcessarPagamentoDaCompra
{
	public sealed class ProcessarPagamentoDaCompraValidator : AbstractValidator<ProcessarPagamentoDaCompraCommand>
	{
		public ProcessarPagamentoDaCompraValidator()
		{
			RuleFor(x => x.UsuarioId)
				.NotEmpty();

			RuleFor(x => x.JogoId)
				.NotEmpty();

			RuleFor(x => x.StatusPagamento)
				.IsInEnum();
		}
	}
}
