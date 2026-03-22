using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogos
{
	public sealed class ListarJogosHandler : IRequestHandler<ListarJogosQuery, IEnumerable<JogoAdminResponse>>
	{
		private readonly IJogoRepository _repository;

		public ListarJogosHandler(IJogoRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<JogoAdminResponse>> Handle(
			ListarJogosQuery request,
			CancellationToken cancellationToken)
		{
			var jogos = await _repository.ObterJogos(somenteAtivos: false, cancellationToken);

			return jogos.Select(j => new JogoAdminResponse(
				Id: j.Id,
				Titulo: j.Titulo,
				Descricao: j.Descricao,
				Preco: j.Preco.Valor,
				DataLancamento: j.DataLancamento,
				Ativo: j.Ativo,
				DataCriacao: j.DataCriacao,
				DataAtualizacao: j.DataAtualizacao
			));
		}
	}
}
