using FCGCatalog.API.Contracts.Review;
using FCGCatalog.API.Contracts.ReviewJogo;
using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Application.Features.Review.CriarReview;
using FCGCatalog.Application.Features.Review.DeletarReview;
using FCGCatalog.Application.Features.Review.EditarReview;
using FCGCatalog.Application.Features.Review.ObterReviewPorId;
using FCGCatalog.Application.Features.Review.ObterReviewsPorJogo;
using FCGCatalog.Application.Features.Review.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers;

[ApiController]
[Route("api/review")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUsuarioContexto _usuarioContexto;

    public ReviewController(IMediator mediator, IUsuarioContexto usuarioContexto)
    {
        _mediator = mediator;
        _usuarioContexto = usuarioContexto;
    }

    /// <summary>
    /// Obtém uma review pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único da review.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna <see cref="StatusCodes.Status200OK"/> com os dados da review ou <see
    /// cref="StatusCodes.Status404NotFound"/> se não encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var query = new ObterReviewPorIdQuery(Id: id);
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Lista todas as avaliações de um jogo específico.
    /// </summary>
    /// <param name="jogoId">Identificador único do jogo.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Resultado da ação contendo a coleção de avaliações do jogo.</returns>
    [HttpGet("jogo/{jogoId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarPorJogo(Guid jogoId, CancellationToken cancellationToken)
    {
        var query = new ObterReviewsPorJogoQuery(jogoId);

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Cria uma nova review de jogo para o usuário autenticado.
    /// </summary>
    /// <param name="request">Os dados da review a ser criada.</param>
    /// <returns>Um resultado 201 Created contendo a review criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] CriarReviewRequest request)
    {
        var usuarioId = _usuarioContexto.ObterUsuarioIdValidado();

        var command = new CriarReviewCommand(
            UsuarioId: usuarioId,
            JogoId: request.JogoId,
            Nota: request.Nota,
            Comentario: request.Comentario
        );
        var response = await _mediator.Send(command);

        return CreatedAtAction(nameof(ReviewController.ObterPorId), new { id = response.Id }, response);
    }

    /// <summary>
    /// Edita uma avaliação existente.
    /// </summary>
    /// <param name="id">O identificador único da avaliação a ser editada.</param>
    /// <param name="request">Os dados atualizados da avaliação.</param>
    /// <param name="cancellationToken">Token para cancelar a operação assíncrona.</param>
    /// <returns>NoContent (204) se a edição foi bem-sucedida, Forbidden (403) se o usuário não tem permissão, ou NotFound (404)
    /// se a avaliação não foi encontrada.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(
        Guid id,
        [FromBody] EditarReviewRequest request,
        CancellationToken cancellationToken)
    {
        var usuarioId = _usuarioContexto.ObterUsuarioIdValidado();

        var command = new EditarReviewCommand(
            Id: id,
            UsuarioId: usuarioId,
            Nota: request.Nota,
            Comentario: request.Comentario
        );

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deleta uma review pelo identificador.
    /// </summary>
    /// <param name="id">O identificador único da review a ser deletada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
    /// <returns>NoContent (204) se a review foi deletada com sucesso; Forbidden (403) se o usuário não tem permissão; NotFound
    /// (404) se a review não foi encontrada.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var usuarioId = _usuarioContexto.ObterUsuarioIdValidado();

        var command = new DeletarReviewCommand(
            Id: id,
            UsuarioId: usuarioId
        );

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}