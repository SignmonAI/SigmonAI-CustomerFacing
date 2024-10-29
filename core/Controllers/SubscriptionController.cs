using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/subscriptions")]
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
        public async Task<IActionResult> DeleteSubscription(
            [FromServices] SubscriptionService service,
            Guid id)
        {
            await service.DeleteSubscription(id);
            return Ok();
        }
    }
}