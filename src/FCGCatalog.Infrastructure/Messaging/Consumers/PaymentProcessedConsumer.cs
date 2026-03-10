using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FCGCatalog.Infrastructure.Messaging.Consumers;

public class PaymentProcessedConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PaymentProcessedConsumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        // Create connection and channel (synchronous API)
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "PaymentProcessedEvent", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            var payment = JsonSerializer.Deserialize<PaymentProcessedEvent>(json);

            if (payment.Approved)
            {
                using var scope = _scopeFactory.CreateScope();

                var service = scope.ServiceProvider.GetRequiredService<IGameLibraryService>();

                await service.AddGameToLibrary(payment.UserId, payment.GameId);
            }
        };

        channel.BasicConsume(queue: "PaymentProcessedEvent", autoAck: true, consumer: consumer);

        // Close channel/connection when stopping
        stoppingToken.Register(() =>
        {
            try
            {
                channel.Close();
                connection.Close();
            }
            catch
            {
                // ignore
            }
        });

        return Task.CompletedTask;
    }
}