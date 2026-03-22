using FCGCatalog.Application.Features.Jogo.ObterJogoPorId;
using MediatR;

namespace FCGCatalog.Application.Features.Jogo.ObterJogo
{
    public sealed record ObterJogoPorIdQuery
    (
        Guid Id,
        bool UsuarioEhAdmin
	) : IRequest<ObterJogoPorIdResponse>;
}
