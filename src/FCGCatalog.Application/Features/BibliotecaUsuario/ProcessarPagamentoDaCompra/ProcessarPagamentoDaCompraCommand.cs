using FCGCatalog.Domain.Enums;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ProcessarPagamentoDaCompra;

public sealed record ProcessarPagamentoDaCompraCommand
(
	Guid UsuarioId,
	Guid JogoId,
	StatusPagamento StatusPagamento
) : IRequest<Unit>;
