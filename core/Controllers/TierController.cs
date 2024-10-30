using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("/api/v1/tiers")]
    public class TierController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTier(          
                [FromServices] TierService service,
                [FromBody] TierCreatePayload payload)
        {
            var result = await service.CreateTier(payload);

            return new CreatedResult("/api/v1/tiers", result);
        }
    }
}