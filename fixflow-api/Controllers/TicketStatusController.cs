using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketStatusController : ControllerBase
    {
        private readonly TicketStatusService _ticketStatusService;

        public TicketStatusController(TicketStatusService ticketStatusService) => _ticketStatusService = ticketStatusService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ticketStatusService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticketStatus = await _ticketStatusService.GetById(id);
            return ticketStatus != null ? Ok(ticketStatus) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticket = await _ticketStatusService.Post(dto.TicketId, dto.StatusId);
            return ticket != null ? Created() : BadRequest();
        }
    }
}
