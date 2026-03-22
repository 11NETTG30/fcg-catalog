namespace FCGCatalog.Application.Abstractions.Security
{
	public static class UsuarioContextoExtensions
	{
		public static Guid ObterUsuarioIdValidado(this IUsuarioContexto contexto)
		{
			return contexto.UsuarioId
				?? throw new UnauthorizedException("Não foi possível identificar o usuário.");
		}

		public static string ObterEmailValidado(this IUsuarioContexto contexto)
		{
			return contexto.Email
				?? throw new UnauthorizedException("Não foi possível identificar o email do usuário.");
		}
	}
}
