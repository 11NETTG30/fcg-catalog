using FCGCatalog.Domain.Shared.Exceptions;

namespace FCGCatalog.Domain.ValueObjects
{
	public sealed class Email
	{
		public string Valor { get; }

		private Email(string valor)
		{
			Valor = valor;
		}

		public static Email Criar(string? valor)
		{
			if (string.IsNullOrWhiteSpace(valor))
				throw new ValidationException("O email não pode ser vazio.");

			if (!EhValido(valor))
				throw new ValidationException("Email inávlido.");

			return new Email(valor.Trim().ToLowerInvariant());
		}

		private static bool EhValido(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}
