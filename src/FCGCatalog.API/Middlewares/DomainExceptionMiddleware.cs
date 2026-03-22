using FCGCatalog.Application.Abstractions.Security;
using FCGCatalog.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FCGCatalog.API.Middlewares;

public class DomainExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ProblemDetailsFactory _problemDetailsFactory;

	public DomainExceptionMiddleware
	(
		RequestDelegate next,
		ProblemDetailsFactory problemDetailsFactory
	)
	{
		_next = next;
		_problemDetailsFactory = problemDetailsFactory;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ValidationException validationException)
		{
			await GerarProblemDetails(
				context,
				StatusCodes.Status400BadRequest,
				"Erro de validação",
				validationException.Message);
		}
		catch (ConflictException conflictException)
		{
			await GerarProblemDetails(
				context,
				StatusCodes.Status409Conflict,
				"Conflito",
				conflictException.Message);
		}
		catch (NotFoundException notFoundException)
		{
			await GerarProblemDetails(
				context,
				StatusCodes.Status404NotFound,
				"Recurso não encontrado",
				notFoundException.Message);
		}
		catch (UnauthorizedException unauthorizedException)
		{
			await GerarProblemDetails(
				context,
				StatusCodes.Status401Unauthorized,
				"Não autorizado",
				unauthorizedException.Message);
		}
		catch (FluentValidation.ValidationException ex)
		{
			Dictionary<string, string[]> errors = ObterErrrosEmDicionarioDoFluentValidation(ex);

			await GerarProblemDetails(
				context,
				StatusCodes.Status400BadRequest,
				"Erro de validação",
				extensions: new Dictionary<string, object>
				{
					["errors"] = errors
				});
		}
	}

	private async Task GerarProblemDetails(
		HttpContext context,
		int statusCode,
		string title,
		string? detail = null,
		IDictionary<string, object>? extensions = null)
	{
		var problemDetails = _problemDetailsFactory.CreateProblemDetails(context);

		problemDetails.Status = statusCode;
		problemDetails.Title = title;
		problemDetails.Detail = detail;

		if (extensions != null)
		{
			foreach (var item in extensions)
			{
				problemDetails.Extensions[item.Key] = item.Value;
			}
		}

		context.Response.StatusCode = statusCode;
		context.Response.ContentType = "application/problem+json";

		await context.Response.WriteAsJsonAsync(problemDetails);
	}

	private static Dictionary<string, string[]> ObterErrrosEmDicionarioDoFluentValidation(FluentValidation.ValidationException ex)
	{
		return ex.Errors
			.GroupBy(e => e.PropertyName)
			.ToDictionary(
				g => g.Key,
				g => g.Select(e => e.ErrorMessage).ToArray()
			);
	}
}

public static class DomainExceptionMiddlewareExtensions
{
	public static void UseDomainExceptionMiddleware(this IApplicationBuilder app)
	{
		app.UseMiddleware<DomainExceptionMiddleware>();
	}
}
