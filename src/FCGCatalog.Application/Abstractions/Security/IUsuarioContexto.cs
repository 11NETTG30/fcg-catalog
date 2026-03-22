namespace FCGCatalog.Application.Abstractions.Security
{
	public interface IUsuarioContexto
	{
		bool EhAdmin { get; }
		Guid? UsuarioId { get; }
		string? Email { get; }
	}
}
