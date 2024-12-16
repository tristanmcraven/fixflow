using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class Ticket
{
    public Guid Guid { get; set; }

    public Guid DeviceBrandGuid { get; set; }

    public Guid DeviceModelGuid { get; set; }

    public Guid DeviceTypeGuid { get; set; }

    public string? ClientFullname { get; set; }

    public string? ClientPhoneNumber { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Note { get; set; }

    public string? Description { get; set; }

    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    public virtual DeviceModel DeviceModel { get; set; } = null!;

    public virtual DeviceType DeviceType { get; set; } = null!;

    public virtual ICollection<TicketKit> TicketKits { get; set; } = new List<TicketKit>();

    public virtual ICollection<TicketMalfunction> TicketMalfunctions { get; set; } = new List<TicketMalfunction>();

    public virtual ICollection<TicketRepair> TicketRepairs { get; set; } = new List<TicketRepair>();

    public virtual ICollection<TicketStatus> TicketStatuses { get; set; } = new List<TicketStatus>();

    public Ticket()
    {
    }

    public Ticket(Guid deviceBrandId, Guid deviceModelId, Guid deviceTypeId, string? clientFullname, string? clientPhoneNumber, DateTime timestamp, string? note, string? description)
    {
        Guid = Guid.NewGuid();
        DeviceBrandGuid = deviceBrandId;
        DeviceModelGuid = deviceModelId;
        DeviceTypeGuid = deviceTypeId;
        ClientFullname = clientFullname;
        ClientPhoneNumber = clientPhoneNumber;
        Timestamp = timestamp;
        Note = note;
        Description = description;
    }
}
