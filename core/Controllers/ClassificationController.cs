using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("/api/v1/classify")]
    public class ClassificationController
    {
        [HttpPost]
        public async Task<IActionResult> ClassifyImage(
            [FromServices] ClassificationService service,
            [FromForm] ClassifyPayload payload)
        {
            var result = await service.Classify(payload);
            return new OkObjectResult(result);
        }
    }
}