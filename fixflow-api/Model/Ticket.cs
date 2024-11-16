using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class Ticket
{
    public uint Id { get; set; }

    public uint DeviceBrandId { get; set; }

    public uint DeviceModelId { get; set; }

    public string? ClientFullname { get; set; }

    public string? ClientPhoneNumber { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Note { get; set; }

    public string? Description { get; set; }

    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    public virtual DeviceModel DeviceModel { get; set; } = null!;

    public virtual ICollection<TicketKit> TicketKits { get; set; } = new List<TicketKit>();

    public virtual ICollection<TicketMalfunction> TicketMalfunctions { get; set; } = new List<TicketMalfunction>();

    public virtual ICollection<TicketRepair> TicketRepairs { get; set; } = new List<TicketRepair>();

    public virtual ICollection<TicketStatus> TicketStatuses { get; set; } = new List<TicketStatus>();

    public Ticket()
    {
    }

    public Ticket(uint deviceBrandId, uint deviceModelId, string? clientFullname, string? clientPhoneNumber, DateTime timestamp, string? note, string? description)
    {
        DeviceBrandId = deviceBrandId;
        DeviceModelId = deviceModelId;
        ClientFullname = clientFullname;
        ClientPhoneNumber = clientPhoneNumber;
        Timestamp = timestamp;
        Note = note;
        Description = description;
    }
}
