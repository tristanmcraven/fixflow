using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class StatusService
    {
        private readonly FixflowContext _context;

        public StatusService(FixflowContext context) => _context = context;

        public async Task<List<Status>> Get()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status?> GetById(uint id)
        {
            return await _context.Statuses.Where(s => s.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Status?> GetByName(string name)
        {
            return await (_context.Statuses.Where(s => s.Name.Equals(name)).FirstOrDefaultAsync());
        }
    }
}
