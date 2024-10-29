using core.Data.Payloads;
using core.Services;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [ApiController]
    [Route("api/v1/bills")]
    public class BillController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBillById(
            [FromServices] BillService service,
            Guid id)
        {
            var bill = await service.GetById(id);

            return Ok(bill);
        }

        [HttpGet]
        [Route("subscription/{id}")]
        public async Task<IActionResult> GetBillBySubscriptionId(
            [FromServices] BillService service,
            Guid subscriptionId)
        {
            var bills = await service.GetBySubscriptionId(subscriptionId);

            return Ok(bills);
        }

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