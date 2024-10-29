using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/media")]
    public class MediaController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMediaById(
            [FromServices] MediaService service,
            Guid id)
        {
            var media = await service.GetById(id);

            return Ok(media);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMedia(
            [FromServices] MediaService service,
            [FromForm] MediaCreatePayload payload)
        {
            var media = await service.CreateMedia(payload);
            
            return Created("/api/v1/medias/register", media);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMedia(
            [FromServices] MediaService service,
            Guid id)
        {
            await service.DeleteMedia(id);

            return Ok();
        }
    }
}