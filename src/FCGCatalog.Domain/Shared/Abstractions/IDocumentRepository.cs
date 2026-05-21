namespace FCGCatalog.Domain.Shared.Abstractions;

public interface IDocumentRepository<T> where T : Entity, IAggregateRoot { }