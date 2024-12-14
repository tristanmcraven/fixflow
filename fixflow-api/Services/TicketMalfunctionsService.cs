using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class TicketMalfunctionsService
    {
        private readonly FixflowContext _context;

        public TicketMalfunctionsService(FixflowContext context) => _context = context;

        public async Task<List<TicketMalfunction>> Get()
        {
            return await _context.TicketMalfunctions.ToListAsync();
        }

        public async Task<TicketMalfunction> Post(Guid ticketId, string Name)
        {
            var ticketMalf = new TicketMalfunction(ticketId, Name);
            _context.TicketMalfunctions.Add(ticketMalf);
            await _context.SaveChangesAsync();
            return ticketMalf;
        }

        public async Task<TicketMalfunction?> Put(Guid ticketMalfId, string name)
        {
            var ticketMalf = _context.TicketMalfunctions.Where(tm => tm.Guid.Equals(ticketMalfId)).FirstOrDefault();
            if (ticketMalf != null)
            {
                ticketMalf.Name = name;
                await _context.SaveChangesAsync();
                return ticketMalf;
            }
            return null;

        }

        public async Task<bool> Delete(Guid ticketMalfId)
        {
            var ticketMalf = await _context.TicketMalfunctions.FindAsync(ticketMalfId);
            if (ticketMalf != null)
            {
                _context.TicketMalfunctions.Remove(ticketMalf);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
