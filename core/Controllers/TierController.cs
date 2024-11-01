using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/tier")]
    public class TierController : ControllerBase
    {
        [HttpPost]
        async public Task<IActionResult> CreateTier(
            [FromServices] TierService service,
            [FromBody] TierCreatePayload payload)
        {
            var result = await service.CreateTier(payload);
            return new OkObjectResult(result);
        }
    }
}