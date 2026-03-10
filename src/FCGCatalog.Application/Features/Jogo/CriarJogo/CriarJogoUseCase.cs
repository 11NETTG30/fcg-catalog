using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using FCGCatalog.Domain.ValueObjects;
using MediatR;
using JogoDomain = FCGCatalog.Domain.Entities.Jogo;

namespace FCGCatalog.Application.Features.Jogo.CriarJogo;

public sealed class CriarJogoUseCase : IRequestHandler<CriarJogoRequest, CriarJogoResponse>
{
	private readonly IJogoRepository _repository;

	public CriarJogoUseCase(IJogoRepository repository)
	{
		_repository = repository;
	}

	public async Task<CriarJogoResponse> Handle(
		CriarJogoRequest request,
		CancellationToken cancellationToken)
	{
		var jogoJaExiste = await _repository.ExistePorTitulo(request.Titulo);

		if (jogoJaExiste)
			throw new ConflictException("Já existe um jogo com esse título.");

		var jogo = JogoDomain.Criar(
			titulo: request.Titulo,
			descricao: request.Descricao,
			preco: Preco.Criar(request.Preco),
			dataLancamento: request.DataLancamento
		);

		await _repository.Adicionar(jogo);

		await _repository.UnitOfWork.Commit();

		return new CriarJogoResponse(jogo.Id);
	}
}