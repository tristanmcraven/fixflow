using fixflow_api.Model;

namespace fixflow_api.Services
{
    public class TicketMalfunctionsService
    {
        private readonly FixflowContext _context;

        public TicketMalfunctionsService(FixflowContext context) => _context = context;
    }
}
