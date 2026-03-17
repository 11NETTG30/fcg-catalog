namespace FCG.Contracts.Events
{
    public sealed record OrderPlacedEvent
    (
        Guid GameId,
        Guid UserId,
        decimal Price
	);
}
