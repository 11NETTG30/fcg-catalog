using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Application.Features.Jogo.ObterJogo;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
	{
        var query = new ObterJogoPorIdQuery(
            Id: id,
            UsuarioEhAdmin: _usuarioContexto.EhAdmin
		);
        var response = await _mediator.Send(query, cancellationToken);

		return Ok(response);
	}
}