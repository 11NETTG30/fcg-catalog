namespace FCG.Contracts.Events
{
    public sealed record class PaymentProcessedEvent
	(
		Guid OrderId,
		Guid PaymentId,
		Guid UserId,
		Guid GameId,
		decimal Price,
		string Status
	);
}
