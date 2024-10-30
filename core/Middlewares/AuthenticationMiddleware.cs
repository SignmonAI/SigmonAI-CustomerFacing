using core.Errors;
using core.Services;

namespace core.Middlewares
{
    public class AuthenticationMiddelware : IMiddleware
    {
        private JwtService _jwtService;

        public AuthenticationMiddelware(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var auth = context.Request.Headers.Authorization.FirstOrDefault()
                    ?? throw new InvalidHeadersException("Missing authorization header.");
            
            var token = auth.Split(" ")[1]
                    ?? throw new InvalidHeadersException("Authorization header should be a bearer token.");

            _jwtService.ValidateToken(token);   

            await next.Invoke(context);
        }
    }
}