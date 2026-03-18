using FCGCatalog.Domain.Enums;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarStatusPagamento;

public sealed record AtualizarStatusPagamentoCommand(
	Guid UsuarioId,
	Guid BibliotecaId,
	StatusPagamento StatusPagamento) : IRequest<Unit>;
