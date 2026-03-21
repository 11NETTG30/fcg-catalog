using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Domain.Shared.Exceptions;

namespace FCGCatalog.Domain.ValueObjects;

public sealed class Preco : ValueObject
{
	public decimal Valor { get; }

	private Preco(decimal valor)
	{
		if (valor < 0)
			throw new ValidationException("Preço não pode ser negativo");

		if (decimal.Round(valor, 2) != valor)
			throw new ValidationException("Preço deve possuir no máximo duas casas decimais");

		Valor = valor;
	}

	public static Preco Criar(decimal valor) => new Preco(valor);

	protected override IEnumerable<object?> ObterComponentesDeIgualdade()
	{
		yield return Valor;
	}
}