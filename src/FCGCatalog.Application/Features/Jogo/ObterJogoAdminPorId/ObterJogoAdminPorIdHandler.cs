using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoAdminPorId
{
	public sealed class ObterJogoAdminPorIdHandler
		: IRequestHandler<ObterJogoAdminPorIdQuery, JogoAdminResponse>
	{
		private readonly IJogoRepository _repository;

		public ObterJogoAdminPorIdHandler(IJogoRepository repository)
		{
			_repository = repository;
		}

		public async Task<JogoAdminResponse> Handle(
			ObterJogoAdminPorIdQuery query,
			CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(query.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {query.Id} não foi encontrado.");

			return new JogoAdminResponse(
				Id: jogo.Id,
				Titulo: jogo.Titulo,
				Descricao: jogo.Descricao,
				Preco: jogo.Preco.Valor,
				DataLancamento: jogo.DataLancamento,
				Ativo: jogo.Ativo,
				DataCriacao: jogo.DataCriacao,
				DataAtualizacao: jogo.DataAtualizacao
			);
		}
	}
}
