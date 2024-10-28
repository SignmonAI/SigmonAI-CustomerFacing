using core.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace core.Middlewares
{
    public record Error(int Status, string Message, Object? Details = null);

    public class ErrorHandlingMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
                HttpContext httpContext,
                Exception exception,
                CancellationToken cancellationToken)
        {
            var error = exception switch
            {
                // AuthenticationException e => new Error(StatusCodes.Status400BadRequest, e.Message, e.Failure),
                // InvalidHeadersException e => new Error(StatusCodes.Status400BadRequest, e.Message),
                // InvalidTokenException e => new Error(StatusCodes.Status400BadRequest, e.Message),
                NotFoundException e => new Error(StatusCodes.Status404NotFound, e.Message),
                UpsertFailException e => new Error(StatusCodes.Status500InternalServerError, e.Message),
                _ => new Error(StatusCodes.Status500InternalServerError, "Unknown server error.")
            };

            httpContext.Response.StatusCode = error.Status;
            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}