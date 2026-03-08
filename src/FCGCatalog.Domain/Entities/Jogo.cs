using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Domain.ValueObjects;

namespace FCGCatalog.Domain.Entities;

public sealed class Jogo : Entity, IAggregateRoot, IAuditavel
{
	private const decimal PRECO_MAXIMO = 10000;

	public string Titulo { get; private set; } = null!;
	public string? Descricao { get; private set; }
	public Preco Preco { get; private set; } = null!;
	public DateTime? DataLancamento { get; private set; }
	public bool Ativo { get; private set; }
	public DateTime DataCriacao { get; private set; }
	public DateTime? DataAtualizacao { get; private set; }

	public Jogo
	(
		string titulo,
		Preco preco,
		string? descricao,
		DateTime? dataLancamento
	)
	{
		SetTitulo(titulo);
		SetPreco(preco);
		SetDescricao(descricao);
		SetDataLancamento(dataLancamento);

		Ativo = true;
	}

	// EF Core
	private Jogo() { }

	public void SetTitulo(string titulo)
	{
		if (string.IsNullOrWhiteSpace(titulo))
			throw new ValidationException("Título do jogo é obrigatório");

		if (titulo.Length is < 5 or > 200)
			throw new ValidationException("Título deve ter entre 5 e 200 caracteres");

		Titulo = titulo.Trim();
	}

	public void SetDescricao(string? descricao)
	{
		if (descricao?.Length > 1000)
			throw new ValidationException("Descrição deve ter no máximo 1000 caracteres");

		Descricao = descricao?.Trim();
	}

	public void SetPreco(Preco preco)
	{
		if (preco.Valor > PRECO_MAXIMO)
			throw new ValidationException(
				$"Preço do jogo excede o limite permitido de {PRECO_MAXIMO:N0}");

		Preco = preco;
	}

	public void SetDataLancamento(DateTime? dataLancamento) =>
		DataLancamento = dataLancamento;

	public void Ativar() =>
		Ativo = true;

	public void Desativar() =>
		Ativo = false;
}