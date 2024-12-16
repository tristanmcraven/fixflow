using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class RepairService
    {
        private readonly FixflowContext _context;

        public RepairService(FixflowContext context) => _context = context;

        public async Task<List<Repair>> Get()
        {
            return await _context.Repairs.ToListAsync();
        }

        public async Task<Repair?> GetByName(string name)
        {
            var repair = await _context.Repairs.Where(r => r.Name.Equals(name)).FirstOrDefaultAsync();
            return repair;
        }

        public async Task<Repair?> GetById(Guid id)
        {
            var repair = await _context.Repairs.Where(r => r.Guid.Equals(id)).FirstOrDefaultAsync();
            return repair;
        }

        public async Task<Repair> Post(string name)
        {
            var repair = new Repair(name);
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

    }
}
