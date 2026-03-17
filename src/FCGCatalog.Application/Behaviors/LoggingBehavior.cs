using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace FCGCatalog.Application.Behaviors
{
	public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

		public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(
			TRequest requisicao,
			RequestHandlerDelegate<TResponse> proximo,
			CancellationToken cancellationToken)
		{
			var nomeRequisicao = typeof(TRequest).Name;

			_logger.LogInformation(
				"Iniciando processamento da requisição {RequestName} com dados: {Request}",
				nomeRequisicao,
				JsonSerializer.Serialize(requisicao)
			);

			var stopwatch = Stopwatch.StartNew();

			try
			{
				var resposta = await proximo();

				stopwatch.Stop();

				_logger.LogInformation(
					"Finalizado processamento da requisição {RequestName} em {ElapsedMilliseconds}ms com resposta: {Response}",
					nomeRequisicao,
					stopwatch.ElapsedMilliseconds,
					JsonSerializer.Serialize(resposta)
				);

				return resposta;
			}
			catch (Exception ex)
			{
				stopwatch.Stop();

				_logger.LogError(
					ex,
					"Erro ao processar requisição {RequestName} após {ElapsedMilliseconds}ms",
					nomeRequisicao,
					stopwatch.ElapsedMilliseconds
				);

				throw;
			}
		}
	}
}
