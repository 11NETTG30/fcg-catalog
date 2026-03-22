using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FCGCatalog.Infrastructure.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers
{
	[ApiController]
	[Route("api/admin/jogos")]
	[Authorize(Roles = RoleNames.Admin)]
	public sealed class JogosAdminController : ControllerBase
	{
		private readonly IMediator _mediator;

		public JogosAdminController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Criar([FromBody] CriarJogoRequest request)
		{
			var command = new CriarJogoCommand(
				Titulo: request.Titulo,
				Descricao: request.Descricao,
				Preco: request.Preco,
				DataLancamento: request.DataLancamento
			);
			var response = await _mediator.Send(command);

			return CreatedAtAction(nameof(JogosController.ObterPorId), new { id = response.Id }, response);
		}
	}
}