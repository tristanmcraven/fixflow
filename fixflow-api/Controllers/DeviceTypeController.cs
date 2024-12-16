using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypeController : ControllerBase
    {
        private readonly DeviceTypeService _service;

        public DeviceTypeController(DeviceTypeService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
        {
            var deviceType = await _service.Post(name);
            return deviceType != null ? Ok(deviceType) : BadRequest();
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var deviceType = await _service.GetById(id);
            return deviceType != null ? Ok(deviceType) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var deviceType = await _service.GetByName(name);
            return deviceType != null? Ok(deviceType) : NotFound();
        }
    }
}
