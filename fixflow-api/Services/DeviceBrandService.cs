using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class DeviceBrandService
    {
        private readonly FixflowContext _context;

        public DeviceBrandService(FixflowContext context) => _context = context;

        public async Task<List<DeviceBrand>> Get()
        {
            return await (_context.DeviceBrands.ToListAsync());
        }

        public async Task<DeviceBrand?> GetById(uint id)
        {
            var brand = await _context.DeviceBrands.Where(db => db.Id.Equals(id)).FirstOrDefaultAsync();
            return brand;
        }

        public async Task<DeviceBrand?> GetByName(string name)
        {
            var brand = await _context.DeviceBrands.Where(db => db.Name.Equals(name)).FirstOrDefaultAsync();
            return brand;
        }

        public async Task<List<DeviceModel>> GetModelsById(uint id)
        {
            var brands = await _context.DeviceModels.Where(dm => dm.DeviceBrandId.Equals(id)).ToListAsync();
            return brands;
        }

        public async Task<List<DeviceModel>> GetModelsByName(string name)
        {
            var brand = await GetByName(name);
            var brands = await _context.DeviceModels.Where(dm => dm.DeviceBrandId.Equals(brand.Id)).ToListAsync();
            return brands;
        }

        public async Task<DeviceBrand> Post(string name)
        {
            var brand = new DeviceBrand(name);
            _context.DeviceBrands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }
    }
}
