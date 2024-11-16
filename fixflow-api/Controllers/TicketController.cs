using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService) => _ticketService = ticketService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ticketService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticket = await _ticketService.Post(dto.DeviceBrandId, dto.DeviceModelId, dto.ClientFullname, dto.ClientPhoneNumber, dto.Timestamp, dto.Note, dto.Description);
            return ticket != null ? Created() : BadRequest(ModelState);
        }
    }
}
