using FCGCatalog.API.Contracts.Jogo;
using FCGCatalog.Application.Features.Jogo.AtivarJogo;
using FCGCatalog.Application.Features.Jogo.CriarJogo;
using FCGCatalog.Application.Features.Jogo.EditarJogo;
using FCGCatalog.Application.Features.Jogo.InativarJogo;
using FCGCatalog.Application.Features.Jogo.ListarJogos;
using FCGCatalog.Application.Features.Jogo.ObterJogoAdminPorId;
using FCGCatalog.Application.Features.Jogo.Shared;
using FCGCatalog.Infrastructure.Shared.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCGCatalog.API.Controllers
{
	[ApiController]
	[Route("api/admin/jogos")]
	public sealed class JogosAdminController : ControllerBase
	{
		private readonly IMediator _mediator;

		public JogosAdminController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(typeof(JogoAdminResponse), StatusCodes.Status201Created)]
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

		[HttpPut("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<IActionResult> Editar(
			Guid id,
			[FromBody] EditarJogoRequest request,
			CancellationToken cancellationToken)
		{
			var command = new EditarJogoCommand(
				Id: id,
				Titulo: request.Titulo,
				Descricao: request.Descricao,
				Preco: request.Preco,
				DataLancamento: request.DataLancamento
			);

			await _mediator.Send(command, cancellationToken);

			return NoContent();
		}

		[HttpPatch("{id:guid}/ativar")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Ativar(Guid id, CancellationToken cancellationToken)
		{
			await _mediator.Send(new AtivarJogoCommand(Id: id), cancellationToken);
			return NoContent();
		}

		[HttpPatch("{id:guid}/desativar")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Desativar(Guid id, CancellationToken cancellationToken)
		{
			await _mediator.Send(new DesativarJogoCommand(Id: id), cancellationToken);
			return NoContent();
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<JogoAdminResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> Listar(CancellationToken cancellationToken)
		{
			var query = new ListarJogosQuery();
			var response = await _mediator.Send(query, cancellationToken);
			return Ok(response);
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(typeof(JogoAdminResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
		{
			var query = new ObterJogoAdminPorIdQuery(Id: id);
			var response = await _mediator.Send(query, cancellationToken);
			return Ok(response);
		}
	}
}