using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;

namespace fixflow_api.Services
{
    public class TicketService
    {
        private readonly FixflowContext _context;

        public TicketService(FixflowContext context) => _context = context;

        public async Task<List<Ticket>> Get()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> Post(uint deviceBrandId,
                                       uint deviceModelId,
                                       string? clientName,
                                       string? clientPhone,
                                       DateTime timestamp,
                                       string? note,
                                       string? description)
        {
            var ticket = new Ticket(deviceBrandId, deviceModelId, clientName, clientPhone, timestamp, note, description);
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
    }
}
