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

        public async Task<List<TicketKit>> GetByTicketId(Guid id)
        {
            return await _context.TicketKits.Where(t => t.TicketGuid.Equals(id)).ToListAsync();
        }

        public async Task<TicketKit> Post(Guid ticketId, string name)
        {
            var ticketKit = new TicketKit(ticketId, name);
            _context.TicketKits.Add(ticketKit);
            await _context.SaveChangesAsync();
            return ticketKit;
        }

        public async Task<TicketKit?> Put(Guid ticketKitId, string name)
        {
            var ticketKit = _context.TicketKits.Where(tk => tk.Guid.Equals(ticketKitId)).FirstOrDefault();
            if (ticketKit != null)
            {
                ticketKit.Name = name;
                await _context.SaveChangesAsync();
                return ticketKit;
            }
            return null;
        }

        public async Task<bool> Delete(Guid ticketKitId)
        {
            var ticketKit = await _context.TicketKits.FindAsync(ticketKitId);
            if (ticketKit != null)
            {
                _context.TicketKits.Remove(ticketKit);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
