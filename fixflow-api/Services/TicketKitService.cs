using fixflow_api.Model;

namespace fixflow_api.Services
{
    public class TicketKitService
    {
        private readonly FixflowContext _context;

        public TicketKitService(FixflowContext context) => _context = context;
    }
}
