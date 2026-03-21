using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoPorId
{
    public sealed class ObterJogoPorIdHandler : IRequestHandler<ObterJogoPorIdQuery, ObterJogoPorIdResponse>
	{
		private readonly IJogoRepository _repository;
		public ObterJogoPorIdHandler(IJogoRepository repository)
		{
			_repository = repository;
		}
		public async Task<ObterJogoPorIdResponse> Handle(
			ObterJogoPorIdQuery query,
			CancellationToken cancellationToken)
		{
			var jogo = await _repository.ObterPorId(query.Id, cancellationToken);

			if (jogo is null)
				throw new NotFoundException($"O jogo de id {query.Id} não foi encontrado.");

			return new ObterJogoPorIdResponse(
				Id: jogo.Id,
				Titulo: jogo.Titulo,
				Descricao: jogo.Descricao,
				Preco: jogo.Preco.Valor,
				DataLancamento: jogo.DataLancamento,
				DataCriacao: jogo.DataCriacao,
				DataAtualizacao: jogo.DataAtualizacao
			);
		}
    }
}