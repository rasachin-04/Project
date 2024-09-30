using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarManagementAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Middleware invoke method to handle the request pipeline
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pass the request to the next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Handle the exception and return a proper response
                await HandleExceptionAsync(context, ex);
            }
        }

        // HandleExceptionAsync method responsible for generating a custom error response
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response status code and content type
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Customize the response message
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please try again later.",
                DetailedMessage = exception.Message // Optionally include the exception message for debugging
            };

            // Return the response as JSON
            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
