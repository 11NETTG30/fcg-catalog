using FCGCatalog.API.Contracts.BibliotecaUsuario;
using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;
using FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;
using FCGCatalog.Infrastructure.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers;

[ApiController]
[Route("api/biblioteca-usuario")]
[Authorize]
public sealed class BibliotecaUsuarioController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IUsuarioContexto _usuarioContexto;

	public BibliotecaUsuarioController(IMediator mediator, IUsuarioContexto usuarioContexto)
	{
		_mediator = mediator;
		_usuarioContexto = usuarioContexto;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> ObterMinhaBiblioteca(CancellationToken cancellationToken)
	{
		var usuarioId = _usuarioContexto.ObterUsuarioIdValidado();
		var query = new ObterBibliotecaUsuarioQuery(usuarioId);

		var response = await _mediator.Send(query, cancellationToken);

		return Ok(response);
	}

	[HttpGet("{usuarioId:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> ObterPorUsuario(Guid usuarioId, CancellationToken cancellationToken)
	{
		var query = new ObterBibliotecaUsuarioQuery(usuarioId);

		var response = await _mediator.Send(query, cancellationToken);

		return Ok(response);
	}

	[HttpPost("comprar")]
	[ProducesResponseType(StatusCodes.Status202Accepted)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Comprar([FromBody] IniciarCompraJogoRequest request, CancellationToken cancellationToken)
	{
		var command = new IniciarCompraJogoCommand(
			UsuarioId: _usuarioContexto.ObterUsuarioIdValidado(),
			JogoId: request.JogoId,
			Email: _usuarioContexto.ObterEmailValidado()
		);

		var response = await _mediator.Send(command, cancellationToken);

		return Accepted(response);
	}

}