using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class TicketStatusService
    {
        private readonly FixflowContext _context;

        public TicketStatusService(FixflowContext context) => _context = context;

        public async Task<List<TicketStatus>> Get()
        {
            return await _context.TicketStatuses.ToListAsync();
        }

        public async Task<TicketStatus> GetById(Guid id)
        {
            return await _context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Guid.Equals(id));
        }

        public async Task<List<TicketStatus>> GetByTicketId(Guid id)
        {
            return await _context.TicketStatuses.Where(ts => ts.TicketGuid.Equals(id)).ToListAsync();
        }

        public async Task<TicketStatus?> Post(Guid ticketId, Guid statusId)
        {
            var ticketStatus = new TicketStatus(ticketId, statusId, DateTime.Now);
            _context.TicketStatuses.Add(ticketStatus);
            await _context.SaveChangesAsync();
            return ticketStatus;
        }
    }
}
