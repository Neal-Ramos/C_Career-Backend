using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.exeptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.middleware
{
    public class GlobalExceptionHandler: IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            // _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var (statusCode, errorCode, message) = exception switch
            {
                InvalidCredentialsExeption => (401, "INVALID_CREDENTIALS", exception.Message),
                InvalidCodeExeption => (401, "INVALID_CODE", exception.Message),
                ExpiredCodeExeption => (401, "EXPIRED_CODE", exception.Message),
                _ => (500, "SERVER_ERROR", "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode,
                errorCode,
                message,
                timestamp = DateTime.UtcNow
            }, cancellationToken);

            return true;
        }
    }
}