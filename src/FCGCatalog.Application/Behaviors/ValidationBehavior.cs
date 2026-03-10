using FluentValidation;
using MediatR;

namespace FCGCatalog.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validadores;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validadores)
	{
		_validadores = validadores;
	}

	public async Task<TResponse> Handle(
		TRequest requisicao,
		RequestHandlerDelegate<TResponse> proximo,
		CancellationToken cancellationToken)
	{
		if (_validadores.Any())
		{
			var contexto = new ValidationContext<TRequest>(requisicao);

			var resultados = await Task.WhenAll(
				_validadores.Select(v => v.ValidateAsync(contexto, cancellationToken))
			);

			var falhas = resultados
				.SelectMany(x => x.Errors)
				.Where(x => x != null)
				.ToList();

			if (falhas.Count != 0)
				throw new ValidationException(falhas);
		}

		return await proximo();
	}
}
