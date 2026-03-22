using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Application.Features.Jogo.ListarJogosDisponiveis;
using FCGCatalog.Application.Features.Jogo.ObterJogo;
using FCGCatalog.Application.Features.Jogo.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers;

[ApiController]
[Route("api/jogos")]
[Authorize]
public sealed class JogosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUsuarioContexto _usuarioContexto;

    public JogosController(IMediator mediator, IUsuarioContexto usuarioContexto)
    {
        _mediator = mediator;
        _usuarioContexto = usuarioContexto;
    }

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(JogoPublicoResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
	{
		var query = new ObterJogoPorIdQuery(Id: id);
		var response = await _mediator.Send(query, cancellationToken);
		return Ok(response);
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<JogoPublicoResponse>), StatusCodes.Status200OK)]
	public async Task<IActionResult> Listar(CancellationToken cancellationToken)
	{
		var query = new ListarJogosDisponiveisQuery();
		var response = await _mediator.Send(query, cancellationToken);
		return Ok(response);
	}
}