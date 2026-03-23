using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.Shared.Uow;
using FCGCatalog.Domain.ValueObjects;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.EditarJogo
{
	public sealed class EditarJogoHandler : IRequestHandler<EditarJogoCommand, Unit>
	{
		private readonly IJogoRepository _repository;

        public EditarJogoHandler(IJogoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(EditarJogoCommand command, CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(command.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {command.Id} não foi encontrado.");

			var jogoJaExiste = await _repository.ExistePorTitulo(command.Titulo, cancellationToken);

			if (jogoJaExiste)
				throw new ConflictException("Já existe um jogo com esse título.");

			jogo.Editar(
				titulo: command.Titulo,
				descricao: command.Descricao,
				preco: Preco.Criar(command.Preco),
				dataLancamento: command.DataLancamento
			);

			_repository.Atualizar(jogo, cancellationToken);

			await _repository.UnitOfWork.Commit(cancellationToken);

			return Unit.Value;
		}
	}
}
