namespace FCGCatalog.Application.Events;

public class PaymentProcessedEvent
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public bool Approved { get; set; }
}