using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
	public sealed class ObterJogoPorIdHandler
		: IRequestHandler<ObterJogoPorIdQuery, JogoPublicoResponse>
	{
		private readonly IJogoRepository _repository;

		public ObterJogoPorIdHandler(IJogoRepository repository)
		{
			_repository = repository;
		}

		public async Task<JogoPublicoResponse> Handle(
			ObterJogoPorIdQuery query,
			CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(query.Id, cancellationToken);

			if (jogo is null || !jogo.Ativo)
				throw new NotFoundException($"O jogo de id {query.Id} não foi encontrado.");

			return new JogoPublicoResponse(
				Id: jogo.Id,
				Titulo: jogo.Titulo,
				Descricao: jogo.Descricao,
				Preco: jogo.Preco.Valor,
				DataLancamento: jogo.DataLancamento
			);
		}
	}
}