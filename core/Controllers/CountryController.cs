using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/countries")]
    public class CountryController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCountryById(
            [FromServices] CountryService service,
            Guid id)
        {
            var country = service.GetById(id);

            return Ok(country);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterCountry(
            [FromServices] CountryService service,
            [FromBody] CountryCreatePayload payload)
        {
            var createdCountry = await service.CreateCountry(payload);

            return Created("/api/v1/countries/register", createdCountry);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCountry(
            [FromServices] CountryService service,
            [FromBody] CountryUpdatePayload payload,
            Guid id)
        {
            var updatedCountry = await service.UpdateCountry(id, payload);

            return Ok(updatedCountry);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCountry(
            [FromServices] CountryService service,
            Guid id)
        {
            await service.DeleteCountry(id);

            return Ok();
        }
    }
}