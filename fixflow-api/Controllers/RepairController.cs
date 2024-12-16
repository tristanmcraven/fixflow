using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private RepairService _service;

        public RepairController(RepairService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var repair = await _service.GetById(id);
            return repair != null ? Ok(repair) : NotFound();
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var repair = await _service.GetByName(name);
            return repair != null ? Ok(repair) : NotFound();
        }

        [HttpPost("name")]
        public async Task<IActionResult> Post(string name)
        {
            var repair = await _service.Post(name);
            return repair != null ? Ok(repair) : BadRequest(); 
        }
    }
}
