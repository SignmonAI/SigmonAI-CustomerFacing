using core.Data.Outbound;
using core.Data.Payloads;
using core.Errors;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> TryLogin(
                [FromServices] LoginService loginService,
                [FromServices] JwtService jwtService,
                [FromBody] LoginPayload payload)
        {
            var result = await loginService.TryLogin(payload);

            return result switch
            {
                LoginResult.Succeeded s => new OkObjectResult(jwtService.GenerateToken(s)),
                LoginResult.Failed => Unauthorized(),
                _ => throw new UnknownServerError(),
            };
        }
    }
}