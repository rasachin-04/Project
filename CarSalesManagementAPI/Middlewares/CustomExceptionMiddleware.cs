using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CarManagementAPI.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Handle the exception globally
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception
            _logger.LogError(exception, "An unhandled exception occurred");

            // Set the response status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create the error response
            var errorResponse = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the middleware. Please contact support."
            };

            // Serialize the response as JSON
            var response = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(response);
        }
    }

    // Error response model
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
