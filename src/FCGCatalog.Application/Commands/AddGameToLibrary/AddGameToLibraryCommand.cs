namespace FCGCatalog.Application.Commands.AddGameToLibrary;

public class AddGameToLibraryCommand
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public decimal Price { get; set; }
}