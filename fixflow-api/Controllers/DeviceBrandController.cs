using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceBrandController : ControllerBase
    {
        private readonly DeviceBrandService _deviceBrandService;

        public DeviceBrandController(DeviceBrandService deviceBrandService) => _deviceBrandService = deviceBrandService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _deviceBrandService.Get());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(uint id)
        {
            var brand = await _deviceBrandService.GetById(id);
            return brand != null ? Ok(brand) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var brand = await _deviceBrandService.GetByName(name);
            return brand != null? Ok(brand) : NotFound();
        }

        [HttpGet("{id:int}/models")]
        public async Task<IActionResult> GetModelsById(uint id)
        {
            var models = await _deviceBrandService.GetModelsById(id);
            return !models.Any() ? NotFound() : Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeviceBrandDto dbdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var brand = await _deviceBrandService.Post(dbdto.Name);
            return brand != null ? Created() : BadRequest();
        }

        [HttpGet("{name}/models")]
        public async Task<IActionResult> GetModelsByName(string name)
        {
            var models = await _deviceBrandService.GetModelsByName(name);
            return !models.Any() ? NotFound() : Ok(models);
        }
    }
}
