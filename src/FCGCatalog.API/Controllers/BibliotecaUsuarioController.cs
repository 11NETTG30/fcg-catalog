using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers;

[ApiController]
[Route("api/biblioteca-usuario")]
public sealed class BibliotecaUsuarioController : ControllerBase
{
	private readonly IMediator _mediator;

	public BibliotecaUsuarioController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("{usuarioId:guid}/comprar")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	public async Task<IActionResult> Comprar(Guid usuarioId, [FromBody] IniciarCompraJogoRequest request)
	{
		var command = new IniciarCompraJogoCommand(usuarioId, request.JogoId);

		await _mediator.Send(command);

		return Accepted();
	}
}