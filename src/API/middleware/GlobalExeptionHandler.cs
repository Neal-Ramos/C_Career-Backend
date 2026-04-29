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
                InvalidInputExeption => (400, "INVALID_INPUTS", exception.Message),
                UnauthorizeExeption => (401, "UNAUTHORIZED", exception.Message),
                NotFoundExeption => (404, "DATA_NOT_FOUND", exception.Message),
                ConflictExeption => (409, "DATABASE_CONFLICT", exception.Message),
                _ => (500, "SERVER_ERROR", exception.Message)
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