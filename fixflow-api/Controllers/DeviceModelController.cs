using fixflow_api.Model.DTO;
using fixflow_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fixflow_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModelController : ControllerBase
    {
        private readonly DeviceModelService _deviceModelService;

        public DeviceModelController(DeviceModelService deviceModelService) => _deviceModelService = deviceModelService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _deviceModelService.Get());
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _deviceModelService.GetById(id);
            return model != null ? Ok(model) : NotFound();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var model = await _deviceModelService.GetByName(name);
            return model != null ? Ok(model) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DeviceModelDTO dmdto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var deviceModel = await _deviceModelService.Post(dmdto.DeviceBrandId, dmdto.Name);
            return deviceModel != null ? Created() : BadRequest(ModelState);
        }


    }
}
