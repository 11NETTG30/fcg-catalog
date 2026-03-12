namespace FCGCatalog.Application.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>(T @event);
}