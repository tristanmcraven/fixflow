using fixflow_api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;

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
                                       string? description,
                                       Guid? ticketId = null)
        {
            Guid guid = ticketId ?? Guid.NewGuid();
            var ticket = new Ticket(guid, deviceBrandId, deviceModelId, deviceTypeId, clientName, clientPhone, timestamp, note, description);
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

        public async Task<List<Ticket>> Search(string q)
        {
            if (string.IsNullOrEmpty(q))
                return new List<Ticket>();

            var keywords = q.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var query = _context.Tickets.AsQueryable();

            foreach (var keyword in keywords)
            {
                query = query.Where(t =>
                EF.Functions.Like(t.DeviceBrand.Name, $"%{keyword}%") ||
                EF.Functions.Like(t.DeviceModel.Name, $"%{keyword}%") ||
                EF.Functions.Like(t.DeviceType.Name, $"%{keyword}%") ||
                EF.Functions.Like(t.ClientFullname, $"%{keyword}%") ||
                EF.Functions.Like(t.ClientPhoneNumber, $"%{keyword}%") ||
                EF.Functions.Like(t.Note, $"%{keyword}%")
                );
            }

            return await query.ToListAsync();
        }

        public async Task<List<Ticket>> Filter(
            Guid? deviceBrandGuid,
            Guid? deviceModelGuid,
            Guid? deviceTypeGuid,
            Guid? statusGuid,
            string clientName,
            string clientPhone,
            DateTime? startDate,
            DateTime? endDate)
        {
            var q = _context.Tickets.AsQueryable();

            if (deviceBrandGuid.HasValue)
                q = q.Where(x => x.DeviceBrandGuid == deviceBrandGuid);

            if (deviceModelGuid.HasValue)
                q = q.Where(x => x.DeviceModelGuid == deviceModelGuid);

            if (deviceTypeGuid.HasValue)
                q = q.Where(x => x.DeviceTypeGuid == deviceTypeGuid);

            if (statusGuid.HasValue)
                q = q.Where(x => x.TicketStatuses.OrderByDescending(y => y.Timestamp).First().StatusGuid == statusGuid);

            if (!String.IsNullOrWhiteSpace(clientName))
                q = q.Where(x => x.ClientFullname.Contains(clientName));

            if (!String.IsNullOrWhiteSpace(clientPhone))
            {
                clientPhone = clientPhone.Replace("+7", "");
                q = q.Where(x => x.ClientPhoneNumber.Contains(clientPhone));
            }

            if (startDate.HasValue)
                q = q.Where(x => x.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                q = q.Where(x => x.Timestamp <= endDate.Value);

            return await q.ToListAsync();
        }

        //public async Task<List<Ticket>> Search(string q)
        //{
        //    var tickets = _context.Tickets.ToList();
        //    var ticketsToReturn = new List<Ticket>();
        //    foreach (var ticket in tickets)
        //    {
        //        var type = 
        //    }
        //}
    }
}
