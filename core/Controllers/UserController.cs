using core.Data.Payloads;
using core.Models;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(
            [FromServices] UserService service,
            [FromBody] UserCreatePayload payload)
        {
            var createdUser = await service.CreateUser(payload);
            
            return Created("/api/v1/users/register", createdUser);
        }
    }
}