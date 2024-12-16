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

        public async Task<DeviceModel?> GetById(Guid id)
        {
            var model = await _context.DeviceModels.Where(m => m.Guid.Equals(id)).FirstOrDefaultAsync();
            return model;
        }

        public async Task<DeviceModel?> GetByName(string name)
        {
            var model = await _context.DeviceModels.Where(m => m.Name.Equals(name)).FirstOrDefaultAsync();
            return model;
        }

        public async Task<DeviceModel> Post(Guid brandDeviceId, string name)
        {
            var deviceModel = new DeviceModel(brandDeviceId, name);
            _context.DeviceModels.Add(deviceModel);
            await _context.SaveChangesAsync();
            return deviceModel;
        }
    }
}
