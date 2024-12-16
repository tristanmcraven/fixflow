using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketStatus
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public Guid StatusGuid { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketStatus()
    {
    }

    public TicketStatus(Guid ticketGuid, Guid statusGuid)
    {
        Guid = Guid.NewGuid();
        TicketGuid = ticketGuid;
        StatusGuid = statusGuid;
        Timestamp = DateTime.Now;
    }
}
