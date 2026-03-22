using FCGCatalog.Application.Features.Jogo.ObterJogoPorId;
using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogo
{
    public sealed record ObterJogoPorIdQuery
    (
        Guid Id
	) : IRequest<JogoPublicoResponse>;
}
