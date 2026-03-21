using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
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

    public JogosController(IMediator mediator)
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

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult ObterPorId(Guid id, CancellationToken cancellationToken)
	{
        var query = new ObterJogoPorIdQuery(id);
        var response = _mediator.Send(query, cancellationToken);

		return Ok(response);
	}
}