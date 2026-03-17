using FCGCatalog.Domain.Shared.Uow;

namespace FCGCatalog.Domain.Shared.Abstractions
{
	public interface IRepository<T> where T : Entity, IAggregateRoot
	{
		IUnitOfWork UnitOfWork { get; }
	}
}
