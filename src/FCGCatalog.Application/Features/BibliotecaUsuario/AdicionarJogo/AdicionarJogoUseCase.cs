using FCGCatalog.Application.Commands.AddGameToLibrary;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AdicionarJogo
{
    public sealed class AdicionarJogoUseCase(
        IMediator mediator) : IRequestHandler<AdicionarJogoRequest, AdicionarJogoResponse>
    {
        private readonly IMediator _mediator = mediator;
        public Task<AdicionarJogoResponse> Handle(AdicionarJogoRequest request, CancellationToken cancellationToken)
        {
            AddGameToLibraryCommand command = new AddGameToLibraryCommand
            {
                UserId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Price = 0
            };
            _mediator.Send(command, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
