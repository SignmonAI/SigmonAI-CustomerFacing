using core.Data.Payloads;
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

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromServices] UserService service,
            [FromBody] UserUpdatePayload payload,
            Guid id)
        {
            var updatedUser = await service.UpdateUser(id, payload);

            return new OkObjectResult(updatedUser);
        }
    }
}