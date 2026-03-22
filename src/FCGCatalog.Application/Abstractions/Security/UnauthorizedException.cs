namespace FCGCatalog.Application.Abstractions.Security
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException(string mensagem) : base(mensagem) { }
	}
}
