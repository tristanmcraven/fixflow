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

        public async Task<Ticket?> GetById(Guid id)
        {
            return await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<List<TicketKit>> GetKits(Guid id)
        {
            var kits = await _context.TicketKits.Where(t => t.TicketGuid.Equals(id)).ToListAsync();
            return kits;
        }

        public async Task<List<TicketStatus>> GetStatuses(Guid id)
        {
            var statuses = await _context.TicketStatuses.Where(t => t.TicketGuid.Equals(id)).ToListAsync();
            return statuses;
        }

        public async Task<List<TicketMalfunction>> GetMalfunctions(Guid id)
        {
            var malfs = await _context.TicketMalfunctions.Where(t => t.TicketGuid.Equals(id)).ToListAsync();
            return malfs;
        }

        public async Task<List<TicketRepair>> GetRepairs(Guid id)
        {
            var repairs = await _context.TicketRepairs.Where(t => t.TicketGuid.Equals(id)).ToListAsync();
            return repairs;
        }

        public async Task<Ticket> Post(Guid deviceBrandId,
                                       Guid deviceModelId,
                                       Guid deviceTypeId,
                                       string? clientName,
                                       string? clientPhone,
                                       DateTime timestamp,
                                       string? note,
                                       string? description)
        {
            var ticket = new Ticket(deviceBrandId, deviceModelId, deviceTypeId, clientName, clientPhone, timestamp, note, description);
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> ChangeNote(Guid id, string note)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.Note = note;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeClientName(Guid id, string clientName)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.ClientFullname = clientName;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeClientPhone(Guid id, string phone)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.ClientPhoneNumber = phone;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }
        public async Task<Ticket?> ChangeDeviceBrand(Guid id, Guid deviceBrandId)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.DeviceBrandGuid = deviceBrandId;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeDeviceModel(Guid id, Guid deviceModelId)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.DeviceModelGuid = deviceModelId;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeDeviceType(Guid id, Guid deviceTypeId)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.DeviceTypeGuid = deviceTypeId;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var ticket = await _context.Tickets.Where(t => t.Guid.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                var ticketKits = await _context.TicketKits.Where(tk => tk.TicketGuid.Equals(id)).ToListAsync();
                var ticketMalfs = await _context.TicketMalfunctions.Where(tk => tk.TicketGuid.Equals(id)).ToListAsync();
                var ticketRepairs = await _context.TicketRepairs.Where(tk => tk.TicketGuid.Equals(id)).ToListAsync();
                var ticketStatuses = await _context.TicketStatuses.Where(tk => tk.TicketGuid.Equals(id)).ToListAsync();

                _context.TicketKits.RemoveRange(ticketKits);
                _context.TicketMalfunctions.RemoveRange(ticketMalfs);
                _context.TicketRepairs.RemoveRange(ticketRepairs);
                _context.TicketStatuses.RemoveRange(ticketStatuses);

                _context.Tickets.Remove(ticket);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
