using MediatR;

namespace FCGCatalog.Application.Features.Jogo.AtivarJogo
{
	public record AtivarJogoCommand(Guid Id) : IRequest<Unit>;
}
