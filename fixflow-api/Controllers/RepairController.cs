using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private RepairService _service;

        public RepairController(RepairService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Post(string name)
        {
            var repair = await _service.Post(name);
            return repair != null ? Created() : BadRequest(); 
        }
    }
}
