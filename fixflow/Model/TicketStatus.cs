using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketStatus
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public uint StatusId { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
