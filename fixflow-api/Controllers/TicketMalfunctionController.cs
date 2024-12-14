using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketMalfunctionController : ControllerBase
    {
        private readonly TicketMalfunctionsService _service;

        public TicketMalfunctionController(TicketMalfunctionsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketMalfunctionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ticketMalf = await _service.Post(dto.TicketId, dto.Name);
            return ticketMalf != null ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(TicketMalfunctionPutDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var ticketMalf = await _service.Put(dto.Id, dto.Name);
            return ticketMalf != null ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _service.Delete(id) ? NoContent() : BadRequest();
        }
    }
}
