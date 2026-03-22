using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogoAdminPorId
{
	public record ObterJogoAdminPorIdQuery(Guid Id) : IRequest<JogoAdminResponse>;
}
