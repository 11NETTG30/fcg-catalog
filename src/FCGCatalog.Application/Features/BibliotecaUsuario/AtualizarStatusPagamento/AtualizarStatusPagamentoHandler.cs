using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Abstractions;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarStatusPagamento;

public sealed class AtualizarStatusPagamentoHandler : IRequestHandler<AtualizarStatusPagamentoCommand, Unit>
{
	private readonly IBibliotecaUsuarioRepository _biblioteca;

	public AtualizarStatusPagamentoHandler(IBibliotecaUsuarioRepository biblioteca)
	{
		_biblioteca = biblioteca;
	}

	public async Task<Unit> Handle(
		AtualizarStatusPagamentoCommand request,
		CancellationToken cancellationToken)
	{
		// Implementação simplificada - você pode melhorar com métodos de atualização no repositório
		throw new NotImplementedException("Método de atualização de status de pagamento não implementado no repositório");
	}
}
