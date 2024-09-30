namespace CarModelManagement
{
	using System.Net;
	using System.Threading.Tasks;
	using Newtonsoft.Json;


	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
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
			var response = new { message = exception.Message };
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
		}
	}

}
