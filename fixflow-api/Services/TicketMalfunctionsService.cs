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

        public async Task<TicketMalfunction?> Put(uint ticketMalfId, string name)
        {
            var ticketMalf = _context.TicketMalfunctions.Where(tm => tm.Id.Equals(ticketMalfId)).FirstOrDefault();
            if (ticketMalf != null)
            {
                ticketMalf.Name = name;
                await _context.SaveChangesAsync();
                return ticketMalf;
            }
            return null;

        }
    }
}
