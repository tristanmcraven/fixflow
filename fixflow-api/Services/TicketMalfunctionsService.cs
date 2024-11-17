using fixflow_api.Model;

namespace fixflow_api.Services
{
    public class TicketMalfunctionsService
    {
        private readonly FixflowContext _context;

        public TicketMalfunctionsService(FixflowContext context) => _context = context;

        public async Task<TicketMalfunction> Post(uint ticketId, string Name)
        {
            var ticketMalf = new TicketMalfunction(ticketId, Name);
            _context.TicketMalfunctions.Add(ticketMalf);
            await _context.SaveChangesAsync();
            return ticketMalf;
        }
    }
}
