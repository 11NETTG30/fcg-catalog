namespace FCGCatalog.Domain.Shared.Uow
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}
