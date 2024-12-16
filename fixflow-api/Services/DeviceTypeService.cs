using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace fixflow_api.Services
{
    public class DeviceTypeService
    {
        private readonly FixflowContext _context;

        public DeviceTypeService(FixflowContext fixflowContext) => _context = fixflowContext;

        public async Task<List<DeviceType>> Get()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        public async Task<DeviceType> Post(string name)
        {
            var deviceType = new DeviceType(name);
            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();
            return deviceType;
        }

        public async Task<DeviceType?> GetById(Guid id)
        {
            var deviceType = await _context.DeviceTypes.Where(dt => dt.Guid.Equals(id)).FirstOrDefaultAsync();
            return deviceType;
        }

        public async Task<DeviceType?> GetByName(string name)
        {
            var deviceType = await _context.DeviceTypes.Where(dt => dt.Name.Equals (name)).FirstOrDefaultAsync();
            return deviceType;
        }



    }
}
