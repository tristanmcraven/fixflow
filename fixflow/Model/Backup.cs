using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Model
{
    public class Backup
    {
        public List<DeviceBrand> DeviceBrands { get; set; }
        public List<DeviceModel> DeviceModels { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<TicketKit> TicketKits { get; set; }
        public List<TicketMalfunction> TicketMalfunctions { get; set; }
        public List<TicketRepair> TicketRepairs { get; set; }
        public List<TicketStatus> TicketStatuses { get; set; }
        public DateTime TimeStamp { get; set; }

        public Backup() { }

        public Backup(List<DeviceBrand> deviceBrands,
                      List<DeviceModel> deviceModels,
                      List<Repair> repairs,
                      List<Status> statuses,
                      List<Ticket> tickets,
                      List<TicketKit> ticketKits,
                      List<TicketMalfunction> ticketMalfunctions,
                      List<TicketRepair> ticketRepairs,
                      List<TicketStatus> ticketStatuses)
        {
            DeviceBrands = deviceBrands;
            DeviceModels = deviceModels;
            Repairs = repairs;
            Statuses = statuses;
            Tickets = tickets;
            TicketKits = ticketKits;
            TicketMalfunctions = ticketMalfunctions;
            TicketRepairs = ticketRepairs;
            TicketStatuses = ticketStatuses;
            TimeStamp = DateTime.Now;
        }
    }
}
