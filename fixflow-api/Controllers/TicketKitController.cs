using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketKitController : ControllerBase
    {
        private readonly TicketKitService _ticketKitService;

        public TicketKitController(TicketKitService ticketKitService) => _ticketKitService = ticketKitService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ticketKitService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticketKit = await _ticketKitService.GetById(id);
            return ticketKit != null ? Ok(ticketKit) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketKitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticketKit = await _ticketKitService.Post(dto.TicketId, dto.Name);
            return ticketKit != null ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(TicketKitPutDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticketKit = await _ticketKitService.Put(dto.TicketKitId, dto.Name);
            return ticketKit != null ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _ticketKitService.Delete(id) ? NoContent() : BadRequest();
        }
    }
}
