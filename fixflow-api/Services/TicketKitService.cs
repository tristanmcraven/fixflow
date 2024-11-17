using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class TicketKitService
    {
        private readonly FixflowContext _context;

        public TicketKitService(FixflowContext context) => _context = context;

        public async Task<List<TicketKit>> GetByTicketId(uint id)
        {
            return await _context.TicketKits.Where(t => t.TicketId.Equals(id)).ToListAsync();
        }

        public async Task<TicketKit> Post(uint ticketId, string name)
        {
            var ticketKit = new TicketKit(ticketId, name);
            _context.TicketKits.Add(ticketKit);
            await _context.SaveChangesAsync();
            return ticketKit;
        }
    }
}
