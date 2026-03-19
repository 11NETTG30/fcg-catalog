using FCGCatalog.Domain.Enums;
using FCGCatalog.Domain.Repositories;
using MediatR;
using BibliotecaUsuarioDomain = FCGCatalog.Domain.Entities.BibliotecaUsuario;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ProcessarPagamentoDaCompra
{
	public sealed class ProcessarPagamentoDaCompraHandler: IRequestHandler<ProcessarPagamentoDaCompraCommand, Unit>
	{
		private readonly IBibliotecaUsuarioRepository _repository;

        public ProcessarPagamentoDaCompraHandler(IBibliotecaUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
			ProcessarPagamentoDaCompraCommand command,
			CancellationToken cancellationToken)
		{
			if (command.StatusPagamento != StatusPagamento.Approved)
				return Unit.Value;

			var item = BibliotecaUsuarioDomain.Criar(
				usuarioId: command.UsuarioId,
				jogoId: command.JogoId
			);

			await _repository.Adicionar(
				item,
				cancellationToken
			);

			await _repository.UnitOfWork.Commit(cancellationToken);

			return Unit.Value;
		}
	}
}
