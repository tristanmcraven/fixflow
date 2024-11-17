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

        [HttpPost]
        public async Task<IActionResult> Post(TicketKitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticketKit = await _ticketKitService.Post(dto.TicketId, dto.Name);
            return ticketKit != null ? Created() : BadRequest();
        }
    }
}
