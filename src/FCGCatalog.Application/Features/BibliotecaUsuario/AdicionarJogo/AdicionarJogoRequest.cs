using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AdicionarJogo
{
    public sealed record AdicionarJogoRequest : IRequest<AdicionarJogoResponse>
    {
    }
}
