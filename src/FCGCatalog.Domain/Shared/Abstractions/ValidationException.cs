using FCGCatalog.Domain.Shared.Exceptions;

namespace FCGCatalog.Domain.Shared.Abstractions
{
	public class ValidationException : DomainException
	{
		public ValidationException()
		{

		}

		public ValidationException(string message) : base(message)
		{

		}

		public ValidationException(string message, Exception innerException) : base(message, innerException)
		{

		}
	}
}
