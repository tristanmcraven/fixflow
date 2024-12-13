using fixflow_api.Model;

namespace fixflow_api.Services
{
    public class RepairService
    {
        private readonly FixflowContext _context;

        public RepairService(FixflowContext context) => _context = context;

        public async Task<Repair> Post(string name)
        {
            var repair = new Repair(name);
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

    }
}
