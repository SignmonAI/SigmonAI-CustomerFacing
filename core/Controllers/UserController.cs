using AutoMapper;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Data.Queries;
using core.Repositories;
using core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(IMapper mapper) : ControllerBase
    {
        private IMapper _mapper = mapper;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(
            [FromServices] UserService service,
            [FromBody] UserCreatePayload payload)
        {
            var createdUser = await service.CreateUser(payload);
            
            var result = new OutboundUser();
            _mapper.Map(createdUser, result);

            return Created("/api/v1/users/register", result);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromServices] UserService service,
            [FromBody] UserUpdatePayload payload,
            Guid id)
        {
            var updatedUser = await service.UpdateUser(id, payload);

            var result = new OutboundUser();
            _mapper.Map(updatedUser, result);

            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchManyUsers(
            [FromServices] UserService service,
            [FromQuery] PaginationQuery pagination)
        {
            var users = await service.FetchManyUsers(pagination);

            var result = new OutboundPaginatedUsers();
            _mapper.Map(users, result);

            return new OkObjectResult(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FetchUser(
            [FromServices] UserService service,
            Guid id)
        {
            var user = await service.FetchUser(id);

            var result = new OutboundUser();
            _mapper.Map(user, result);

            return new OkObjectResult(JsonConvert.SerializeObject(result));
        }
    }
}