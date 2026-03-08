using FCGCatalog.Domain.Shared.Abstractions;

namespace FCGCatalog.Domain.ValueObjects;

public sealed class Preco : ValueObject
{
	public decimal Valor { get; }

	public Preco(decimal valor)
	{
		if (valor < 0)
			throw new ValidationException("Preço não pode ser negativo");

		if (decimal.Round(valor, 2) != valor)
			throw new ValidationException("Preço deve possuir no máximo duas casas decimais");

		Valor = valor;
	}

	protected override IEnumerable<object?> ObterComponentesDeIgualdade()
	{
		yield return Valor;
	}
}