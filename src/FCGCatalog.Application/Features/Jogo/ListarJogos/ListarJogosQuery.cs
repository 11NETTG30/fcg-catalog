using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogos
{
	public record ListarJogosQuery() : IRequest<IEnumerable<JogoAdminResponse>>;

}
