using FCGCatalog.Application.Events;
using FCGCatalog.Application.Interfaces;
using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.AdicionarJogo
{
    public sealed class AdicionarJogoUseCase(
        IEventBus eventBus) : IRequestHandler<AdicionarJogoRequest, AdicionarJogoResponse>
    {
        private readonly IEventBus _eventBus = eventBus;

        public Task<AdicionarJogoResponse> Handle(AdicionarJogoRequest request, CancellationToken cancellationToken)
        {
            // Adicionar lógica de verificações e tals
            var orderEvent = new OrderPlacedEvent
            {
                UserId = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Price = 0
            };
            _eventBus.PublishAsync(orderEvent);
            throw new NotImplementedException();
        }
    }
}
