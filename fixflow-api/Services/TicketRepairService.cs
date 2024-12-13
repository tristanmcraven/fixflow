using fixflow_api.Model;
using System.Runtime.CompilerServices;

namespace fixflow_api.Services
{
    public class TicketRepairService
    {
        private readonly FixflowContext _context;

        public TicketRepairService(FixflowContext context)
        {
            _context = context;
        }

        public async Task<TicketRepair> Post(uint ticketId, uint repairId, int? price)
        {
            var ticketRepair = new TicketRepair(ticketId, repairId, price);
            _context.TicketRepairs.Add(ticketRepair);
            await _context.SaveChangesAsync();
            return ticketRepair;
        }
    }
}
