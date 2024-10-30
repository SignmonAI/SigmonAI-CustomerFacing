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
                [FromServices] LoginService service,
                [FromBody] LoginPayload payload)
        {
            var result = await service.TryLogin(payload);

            return result switch
            {
                LoginResult.Succeeded => new OkObjectResult(result),
                LoginResult.Failed => Unauthorized(),
                _ => throw new UnknownServerError(),
            };
        }
    }
}