using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _ticketService.GetById(id);
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpGet("{id:Guid}/kits")]
        public async Task<IActionResult> GetKits(Guid id)
        {
            var kits = await _ticketService.GetKits(id);
            return Ok(kits);
        }

        [HttpGet("{id:Guid}/statuses")]
        public async Task<IActionResult> GetStatuses(Guid id)
        {
            var statuses = await _ticketService.GetStatuses(id);
            return Ok(statuses);
        }

        [HttpGet("{id:Guid}/malfunctions")]
        public async Task<IActionResult> GetMalfunctions(Guid id)
        {
            var malfs = await _ticketService.GetMalfunctions(id);
            return Ok(malfs);
        }

        [HttpGet("{id:Guid}/repairs")]
        public async Task<IActionResult> GetRepairs(Guid id)
        {
            var repairs = await _ticketService.GetRepairs(id);
            return Ok(repairs);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticket = await _ticketService.Post(dto.DeviceBrandId, dto.DeviceModelId, dto.DeviceTypeId, dto.ClientFullname, dto.ClientPhoneNumber, dto.Timestamp, dto.Note, dto.Description);
            return ticket != null ? Ok(ticket) : BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeNote(Guid id, string note)
        {
            var ticket = await _ticketService.ChangeNote(id, note);
            return ticket != null ? Ok(ticket) : BadRequest();
        }

        [HttpPut("{id}/changeclientname")]
        public async Task<IActionResult> ChangeClientName(TicketDto.ChangeName dto)
        {
            var ticket = await _ticketService.ChangeClientName(dto.TicketId, dto.Name);
            return ticket != null ? NoContent() : BadRequest();
        }

        [HttpPut("{id}/changeclientphone")]
        public async Task<IActionResult> ChangeClientPhone(TicketDto.ChangePhone dto)
        {
            var ticket = await _ticketService.ChangeClientPhone(dto.TicketId, dto.Phone);
            return ticket != null ? NoContent() : BadRequest();
        }

        [HttpPut("{id}/changedevicebrand")]
        public async Task<IActionResult> ChangeDeviceBrand(TicketDto.ChangeDeviceBrand dto)
        {
            var ticket = await _ticketService.ChangeDeviceBrand(dto.TicketId, dto.DeviceBrandId);
            return ticket != null ? NoContent() : BadRequest();
        }

        [HttpPut("{id}/changedevicemodel")]
        public async Task<IActionResult> ChangeDeviceModel(TicketDto.ChangeDeviceModel dto)
        {
            var ticket = await _ticketService.ChangeDeviceModel(dto.TicketId, dto.DeviceModelId);
            return ticket != null ? NoContent() : BadRequest();
        }

        [HttpPut("{id}/changedevicetype")]
        public async Task<IActionResult> ChangeDeviceType(TicketDto.ChangeDeviceType dto)
        {
            var ticket = await _ticketService.ChangeDeviceType(dto.TicketId, dto.DeviceTypeId);
            return ticket != null ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _ticketService.Delete(id) ? NoContent() : BadRequest();
        }
    }
}
