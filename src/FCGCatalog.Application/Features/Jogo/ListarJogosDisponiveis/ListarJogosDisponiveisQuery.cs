using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ListarJogosDisponiveis
{
	public record ListarJogosDisponiveisQuery() : IRequest<IEnumerable<JogoPublicoResponse>>;

}
