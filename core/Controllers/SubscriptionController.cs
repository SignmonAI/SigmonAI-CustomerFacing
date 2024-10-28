using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/subscription")]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterSubscription(
            [FromServices] SubscriptionService service,
            [FromBody] SubscriptionCreatePayload payload)
        {
            var createdSubscription = await service.CreateSubscription(payload);
            
            return Created("/api/v1/subscription/register", createdSubscription);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromServices] SubscriptionService service,
            Guid id)
        {
            await service.DeleteSubscription(id);
            return NoContent();
        }
    }
}