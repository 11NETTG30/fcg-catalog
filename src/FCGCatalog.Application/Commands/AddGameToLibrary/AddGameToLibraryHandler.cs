using FCGCatalog.Application.Events;
using FCGCatalog.Application.Interfaces;

namespace FCGCatalog.Application.Commands.AddGameToLibrary;

public class AddGameToLibraryHandler
{
    private readonly IEventBus _eventBus;

    public AddGameToLibraryHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(AddGameToLibraryCommand command)
    {
        var orderEvent = new OrderPlacedEvent
        {
            UserId = command.UserId,
            GameId = command.GameId,
            Price = command.Price
        };

        await _eventBus.PublishAsync(orderEvent);
    }
}