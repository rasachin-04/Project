using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ErrorHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request." });
		return context.Response.WriteAsync(result);
	}
}
