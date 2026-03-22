using FCGCatalog.Application.Abstractions.Security;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FCGCatalog.Infrastructure.Shared.Security
{
	public class UsuarioContexto : IUsuarioContexto
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UsuarioContexto(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;


		public bool EhAdmin => User?.IsInRole(RoleNames.Admin) ?? false;

		public Guid? UsuarioId
		{
			get
			{
				var sub = User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
				return Guid.TryParse(sub, out var guid) ? guid : null;
			}
		}

		public string? Email => User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

	}
}