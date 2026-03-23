using FCG.Shared.Contracts.Events;
using FCGCatalog.Application.Features.BibliotecaUsuario.LiberarJogoParaUsuario;
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

            if (status != StatusPagamento.Approved)
            {
                _logger.LogInformation(
                    "Pagamento não aprovado para o pedido {OrderId}. Status: {Status}. Nenhuma ação tomada.",
                    message.OrderId,
                    status);
                return;
            }

            var command = new LiberarJogoParaUsuarioCommand(
                UsuarioId: message.UserId,
                JogoId: message.GameId
            );

            await _mediator.Send(command, context.CancellationToken);
        }
    }
}
