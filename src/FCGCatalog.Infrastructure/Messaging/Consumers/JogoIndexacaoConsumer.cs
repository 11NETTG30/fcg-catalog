using FCGCatalog.Application.Abstractions.Messaging.Events;
using FCGCatalog.Infrastructure.Search;
using MassTransit;

namespace FCGCatalog.Infrastructure.Messaging.Consumers
{
	public sealed class JogoIndexacaoConsumer :
		IConsumer<JogoCriadoEvent>,
		IConsumer<JogoAlteradoEvent>
	{
		private readonly IIndiceJogoService _indiceJogoService;

		public JogoIndexacaoConsumer(IIndiceJogoService indiceJogoService)
		{
			_indiceJogoService = indiceJogoService;
		}

		public Task Consume(ConsumeContext<JogoCriadoEvent> context)
			=> _indiceJogoService.IndexarAsync(Mapear(context.Message), context.CancellationToken);

		public Task Consume(ConsumeContext<JogoAlteradoEvent> context)
			=> _indiceJogoService.IndexarAsync(Mapear(context.Message), context.CancellationToken);

		private static JogoParaIndexar Mapear(JogoEventBase evento) =>
			new(evento.Id, evento.Titulo, evento.Descricao, evento.Preco,
				evento.DataLancamento, evento.Ativo, evento.DataCriacao, evento.DataAtualizacao);
	}
}
