using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogosDisponiveis
{
	public sealed class ListarJogosDisponiveisHandler : IRequestHandler<ListarJogosDisponiveisQuery, IEnumerable<JogoPublicoResponse>>
	{
		private readonly IJogoRepository _repository;

		public ListarJogosDisponiveisHandler(IJogoRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<JogoPublicoResponse>> Handle(
			ListarJogosDisponiveisQuery request,
			CancellationToken cancellationToken)
		{
			var jogos = await _repository.ObterJogos(somenteAtivos: true, cancellationToken);

			return jogos.Select(j => new JogoPublicoResponse(
				Id: j.Id,
				Titulo: j.Titulo,
				Descricao: j.Descricao,
				Preco: j.Preco.Valor,
				DataLancamento: j.DataLancamento
			));
		}
	}
}
