using core.Data.Payloads;
using core.Data.Queries;
using core.Repositories;
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

        [HttpGet]
        public async Task<IActionResult> FetchManyUsers(
            [FromServices] UserService service,
            [FromQuery] PaginationQuery pagination)
        {
            var users = await service.FetchManyUsers(pagination);

            return new OkObjectResult(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FetchUser(
            [FromServices] UserService service,
            Guid id)
        {
            var user = await service.FetchUser(id);

            return new OkObjectResult(user);
        }
    }
}