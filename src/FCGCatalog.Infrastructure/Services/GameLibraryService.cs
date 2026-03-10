using FCGCatalog.Application.Interfaces;

namespace FCGCatalog.Infrastructure.Services;

public class GameLibraryService : IGameLibraryService
{
    public Task AddGameToLibrary(Guid userId, Guid gameId)
    {
        // salvar no banco
        return Task.CompletedTask;
    }
}