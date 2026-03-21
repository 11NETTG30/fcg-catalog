using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.ValueObjects;
using MediatR;
using JogoDomain = FCGCatalog.Domain.Entities.Jogo;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed class CriarJogoHandler : IRequestHandler<CriarJogoCommand, CriarJogoResponse>
{
	private readonly IJogoRepository _repository;

	public CriarJogoHandler(IJogoRepository repository)
	{
		_repository = repository;
	}

	public async Task<CriarJogoResponse> Handle(
		CriarJogoCommand command,
		CancellationToken cancellationToken)
	{
		var jogoJaExiste = await _repository.ExistePorTitulo(command.Titulo, cancellationToken);

		if (jogoJaExiste)
			throw new ConflictException("Já existe um jogo com esse título.");

		var jogo = JogoDomain.Criar(
			titulo: command.Titulo,
			descricao: command.Descricao,
			preco: Preco.Criar(command.Preco),
			dataLancamento: command.DataLancamento
		);

		await _repository.Adicionar(jogo, cancellationToken);
		await _repository.UnitOfWork.Commit(cancellationToken);

		return new CriarJogoResponse(jogo.Id);
	}
}