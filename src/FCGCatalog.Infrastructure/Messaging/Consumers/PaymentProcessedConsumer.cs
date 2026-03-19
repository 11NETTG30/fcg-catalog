using FCG.Contracts.Events;
using FCGCatalog.Application.Features.BibliotecaUsuario.ProcessarPagamentoDaCompra;
using FCGCatalog.Domain.Enums;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FCGCatalog.Infrastructure.Messaging.Consumers
{
	public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<PaymentProcessedConsumer> _logger;

		public PaymentProcessedConsumer(
			IMediator mediator,
			ILogger<PaymentProcessedConsumer> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
		{
			var message = context.Message;
			var status = Enum.Parse<StatusPagamento>(message.Status);

			var command = new ProcessarPagamentoDaCompraCommand(
				UsuarioId: message.UserId,
				JogoId: message.GameId,
				StatusPagamento: status
			);

			await _mediator.Send(command, context.CancellationToken);
		}
	}
}
