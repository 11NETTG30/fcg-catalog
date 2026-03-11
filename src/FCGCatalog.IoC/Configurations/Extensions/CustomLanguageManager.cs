using FluentValidation.Resources;

namespace FCGCatalog.IoC.Padroes.Extensions;

internal class CustomLanguageManager : LanguageManager
{
	private const string _PT_BR = "pt-BR";

	public CustomLanguageManager()
	{
		AddTranslation(_PT_BR, "MinimumLengthValidator",
			"'{PropertyName}' deve ter no mínimo {MinLength} caracteres. Você digitou {TotalLength} caracteres.");

		AddTranslation(_PT_BR, "MaximumLengthValidator",
			"'{PropertyName}' deve ter no máximo {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
	}
}
