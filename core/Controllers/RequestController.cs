using core.Data.Outbound;
using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/request")]
    public class RequestController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRequestById(
            [FromServices] RequestService service,
            Guid id)
        {
            var request = await service.GetById(id);

            return Ok(OutboundRequest.BuildFromEntity(request));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRequestsByUserId(
            [FromServices] RequestService service,
            Guid user)
        {
            var requests = await service.GetByUserId(user);

            return Ok(requests.Select(r => 
                OutboundRequest.BuildFromEntity(r)));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRequest(
            [FromServices] RequestService service,
            [FromBody] RequestCreatePayload payload)
        {
            var request = await service.CreateRequest(payload);
            
            return Created("/api/v1/requests/register", 
                OutboundRequest.BuildFromEntity(request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRequest(
            [FromServices] RequestService service,
            Guid id)
        {
            await service.DeleteRequest(id);

            return Ok();
        }
    }
}