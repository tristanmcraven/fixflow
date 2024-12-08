using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task<Ticket?> GetById(uint id)
        {
            return await _context.Tickets.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<List<TicketKit>> GetKits(uint id)
        {
            var kits = await _context.TicketKits.Where(t => t.TicketId.Equals(id)).ToListAsync();
            return kits;
        }

        public async Task<List<TicketStatus>> GetStatuses(uint id)
        {
            var statuses = await _context.TicketStatuses.Where(t => t.TicketId.Equals(id)).ToListAsync();
            return statuses;
        }

        public async Task<List<TicketMalfunction>> GetMalfunctions(uint id)
        {
            var malfs = await _context.TicketMalfunctions.Where(t => t.TicketId.Equals(id)).ToListAsync();
            return malfs;
        }

        public async Task<List<TicketRepair>> GetRepairs(uint id)
        {
            var repairs = await _context.TicketRepairs.Where(t => t.TicketId.Equals(id)).ToListAsync();
            return repairs;
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

        public async Task<Ticket?> ChangeNote(uint id, string note)
        {
            var ticket = await _context.Tickets.Where(t=> t.Id.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.Note = note;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeClientName(uint id, string clientName)
        {
            var ticket = await _context.Tickets.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.ClientFullname = clientName;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeClientPhone(uint id, string phone)
        {
            var ticket = await _context.Tickets.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.ClientPhoneNumber = phone;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }
        public async Task<Ticket?> ChangeDeviceBrand(uint id, uint deviceBrandId)
        {
            var ticket = await _context.Tickets.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.DeviceBrandId = deviceBrandId;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        public async Task<Ticket?> ChangeDeviceModel(uint id, uint deviceModelId)
        {
            var ticket = await _context.Tickets.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
            if (ticket != null)
            {
                ticket.DeviceModelId = deviceModelId;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }
    }
}
