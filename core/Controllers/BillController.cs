using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/bills")]
    public class BillController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterBill(
            [FromServices] BillService service,
            [FromBody] BillCreatePayload payload)
        {
            var createdBill = await service.CreateBill(payload);
            
            return Created("/api/v1/bills/register", createdBill);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBill(
            [FromServices] BillService service,
            [FromBody] BillUpdatePayload payload,
            Guid id)
        {
            var updatedBill = await service.UpdateBill(id, payload);
            return Ok(updatedBill);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBill(
            [FromServices] BillService service,
            Guid id)
        {
            await service.DeleteBill(id);
            return Ok();
        }
    }
}