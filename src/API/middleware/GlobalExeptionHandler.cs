using Application.commons.Helpers;
using Application.exeptions;
using Microsoft.AspNetCore.Diagnostics;

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

            var (statusCode, errorCode, message) = exception switch
            {
                InvalidInputExeption => (401, "INVALID_INPUTS", exception.Message),
                NotFoundExeption => (404, "DATA_NOT_FOUND", exception.Message),
                _ => (500, "SERVER_ERROR", "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode,
                errorCode,
                message,
                timestamp = DateHelper.GetPHTime()
            }, cancellationToken);

            return true;
        }
    }
}