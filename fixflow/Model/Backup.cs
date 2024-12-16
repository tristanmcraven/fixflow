using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Model
{
    public class Backup
    {
        public List<DeviceBrand> DeviceBrands = new();
        public List<DeviceModel> DeviceModels = new();
        public List<DeviceType> DeviceTypes = new();
        public List<Repair> Repairs = new();
        public List<Status> Statuses = new();
        public List<Ticket> Tickets = new();
        public List<TicketKit> TicketKits = new();
        public List<TicketMalfunction> TicketMalfunctions = new();
        public List<TicketRepair> TicketRepairs = new();
        public List<TicketStatus> TicketStatuses = new();
        public DateTime TimeStamp = new();

        public Backup() { }

        public Backup(List<DeviceBrand> deviceBrands,
                      List<DeviceModel> deviceModels,
                      List<DeviceType> deviceTypes,
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
            DeviceTypes = deviceTypes;
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
