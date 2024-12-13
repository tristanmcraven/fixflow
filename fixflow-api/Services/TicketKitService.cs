using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class TicketKitService
    {
        private readonly FixflowContext _context;

        public TicketKitService(FixflowContext context) => _context = context;

        public async Task<List<TicketKit>>Get()
        {
            return await _context.TicketKits.ToListAsync();
        }

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

        public async Task<TicketKit?> Put(uint ticketKitId, string name)
        {
            var ticketKit = _context.TicketKits.Where(tk => tk.Id.Equals(ticketKitId)).FirstOrDefault();
            if (ticketKit != null)
            {
                ticketKit.Name = name;
                await _context.SaveChangesAsync();
                return ticketKit;
            }
            return null;
        }
    }
}
