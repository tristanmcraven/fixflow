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
    }
}
