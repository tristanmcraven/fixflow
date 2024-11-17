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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(uint id)
        {
            var ticket = await _ticketService.GetById(id);
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpGet("{id:int}/kits")]
        public async Task<IActionResult> GetKits(uint id)
        {
            var kits = await _ticketService.GetKits(id);
            return Ok(kits);
        }

        [HttpGet("{id:int}/statuses")]
        public async Task<IActionResult> GetStatuses(uint id)
        {
            var statuses = await _ticketService.GetStatuses(id);
            return Ok(statuses);
        }

        [HttpGet("{id:int}/malfunctions")]
        public async Task<IActionResult> GetMalfunctions(uint id)
        {
            var malfs = await _ticketService.GetMalfunctions(id);
            return Ok(malfs);
        }

        [HttpGet("{id:int}/repairs")]
        public async Task<IActionResult> GetRepairs(uint id)
        {
            var repairs = await _ticketService.GetRepairs(id);
            return Ok(repairs);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticket = await _ticketService.Post(dto.DeviceBrandId, dto.DeviceModelId, dto.ClientFullname, dto.ClientPhoneNumber, dto.Timestamp, dto.Note, dto.Description);
            return ticket != null ? Ok(ticket) : BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeNote(uint id, string note)
        {
            var ticket = await _ticketService.ChangeNote(id, note);
            return ticket != null ? Ok(ticket) : BadRequest();
        }
    }
}
