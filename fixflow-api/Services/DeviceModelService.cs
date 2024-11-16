using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class DeviceModelService
    {
        private readonly FixflowContext _context;

        public DeviceModelService(FixflowContext context) => _context = context;

        public async Task<List<DeviceModel>> Get()
        {
            return await _context.DeviceModels.ToListAsync();
        }
    }
}
