using AutoMapper;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Data.Queries;
using core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/countries")]
    public class CountryController(IMapper mapper) : ControllerBase
    {
        private IMapper _mapper = mapper;

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCountryById(
            [FromServices] CountryService service,
            Guid id)
        {
            var country = await service.GetById(id);    

            return Ok(country);
        }

        [HttpGet]
        public async Task<IActionResult> FetchManyCountries(
            [FromServices] CountryService service,
            [FromQuery] PaginationQuery pagination)
        {
            var countries = await service.FetchManyCountries(pagination);

            var result = new OutboundPaginatedCountries();
            _mapper.Map(countries, result);

            return new OkObjectResult(JsonConvert.SerializeObject(result));            
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