using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Features.Jogo.ComprarJogo;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers;

[ApiController]
[Route("api/jogos")]
public sealed class JogosController : ControllerBase
{
	private readonly IMediator _mediator;

	public JogosController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<IActionResult> Criar([FromBody] CriarJogoCommand request)
	{
		var response = await _mediator.Send(request);

		return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult ObterPorId(Guid id)
	{
		throw new NotImplementedException();
	}

	[HttpPost("{id:guid}/comprar")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	public async Task<IActionResult> Comprar(Guid id, [FromBody] IniciarCompraJogoRequest request)
	{
		var command = new IniciarCompraJogoCommand(id, request.UsuarioId);

		await _mediator.Send(command);

		return Accepted();
	}
}