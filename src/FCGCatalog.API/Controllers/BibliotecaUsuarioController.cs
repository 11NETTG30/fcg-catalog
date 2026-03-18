using FCGCatalog.API.Contracts.Jogo;
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

    public BibliotecaUsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{usuarioId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorUsuario(Guid usuarioId)
    {
        var query = new ObterBibliotecaUsuarioQuery(usuarioId);

        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("{usuarioId:guid}/comprar")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Comprar(Guid usuarioId, [FromBody] IniciarCompraJogoRequest request)
    {
        var command = new IniciarCompraJogoCommand(usuarioId, request.JogoId);

        await _mediator.Send(command);

        return Accepted();
    }

    [HttpPut("{usuarioId:guid}/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid usuarioId, Guid id, [FromBody] AtualizarBibliotecaUsuarioRequest request)
    {
        var command = new AtualizarBibliotecaUsuarioCommand(usuarioId, id, request.JogoId);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{usuarioId:guid}/{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarStatus(Guid usuarioId, Guid id, [FromBody] AtualizarStatusPagamentoRequest request)
    {
        var command = new AtualizarStatusPagamentoCommand(usuarioId, id, request.StatusPagamento);

        await _mediator.Send(command);

        return NoContent();
    }

}