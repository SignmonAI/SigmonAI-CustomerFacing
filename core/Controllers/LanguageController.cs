using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/languages")]
    public class LanguageController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLanguageById(
            [FromServices] LanguageService service,
            Guid id)
        {
            var language = await service.GetById(id);

            return Ok(language);
        }

        [HttpGet]
        [Route("country/{countryId}")]
        public async Task<IActionResult> GetLanguageByCountryId(
            [FromServices] LanguageService service,
            Guid countryId)
        {
            var language = await service.GetByCountryId(countryId);

            return Ok(language);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterLanguage(
            [FromServices] LanguageService service,
            [FromBody] LanguageCreatePayload payload)
        {
            var createdLanguage = await service.CreateLanguage(payload);

            return Created("/api/v1/languages/register", createdLanguage);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateLanguage(
            [FromServices] LanguageService service,
            [FromBody] LanguageUpdatePayload payload,
            Guid id)
        {
            var updatedLanguage = await service.UpdateLanguage(id, payload);

            return Ok(updatedLanguage);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLanguage(
            [FromServices] LanguageService service,
            Guid id)
        {
            await service.DeleteLanguage(id);

            return Ok();
        }
    }
}