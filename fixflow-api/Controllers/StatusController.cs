using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService) => _statusService = statusService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _statusService.Get());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(uint id)
        {
            var status = await _statusService.GetById(id);
            return status != null ? Ok(status) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var status = await _statusService.GetByName(name);
            return status != null ? Ok(status) : NotFound();
        }
    }
}
