using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class TicketStatus
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public Guid StatusGuid { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketStatus()
    {
    }

    public TicketStatus(Guid ticketId, Guid statusId, DateTime? timestamp, Guid id)
    {
        Guid = id;
        TicketGuid = ticketId;
        StatusGuid = statusId;
        Timestamp = timestamp;
    }
}
