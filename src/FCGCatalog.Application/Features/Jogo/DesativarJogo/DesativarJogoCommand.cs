using MediatR;

namespace FCGCatalog.Application.Features.Jogo.InativarJogo
{
	public record DesativarJogoCommand(Guid Id) : IRequest<Unit>;
}
