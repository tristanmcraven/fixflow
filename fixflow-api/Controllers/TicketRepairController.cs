using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketRepairController : ControllerBase
    {
        private readonly TicketRepairService _ticketRepairService;

        public TicketRepairController(TicketRepairService ticketRepairService)
        {
            _ticketRepairService = ticketRepairService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TicketRepairDto dto)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var ticket = await _ticketRepairService.Post(dto.TicketId, dto.TicketId, dto.Price);
            return ticket != null ? Created() : BadRequest(ModelState);
        }
    }
}
