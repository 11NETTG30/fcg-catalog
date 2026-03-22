using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarBibliotecaUsuario;
using FCGCatalog.Application.Features.BibliotecaUsuario.AtualizarStatusPagamento;
using FCGCatalog.Application.Features.BibliotecaUsuario.IniciarCompraJogo;
using FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;
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

    [HttpGet("{usuarioId:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
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

	[HttpPut("{usuarioId:guid}/{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Atualizar(Guid usuarioId, Guid id, [FromBody] AtualizarBibliotecaUsuarioRequest request, CancellationToken cancellationToken)
	{
		var command = new AtualizarBibliotecaUsuarioCommand(usuarioId, id, request.JogoId);

		await _mediator.Send(command, cancellationToken);

		return NoContent();
	}

	[HttpPatch("{usuarioId:guid}/{id:guid}/status")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AtualizarStatus(Guid usuarioId, Guid id, [FromBody] AtualizarStatusPagamentoRequest request, CancellationToken cancellationToken)
	{
		var command = new AtualizarStatusPagamentoCommand(usuarioId, id, request.StatusPagamento);

		await _mediator.Send(command, cancellationToken);

		return NoContent();
	}

}