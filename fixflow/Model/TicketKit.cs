using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketKit
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketKit()
    {
    }

    public TicketKit(Guid ticketGuid, string name)
    {
        Guid = Guid.NewGuid();
        TicketGuid = ticketGuid;
        Name = name;
    }
}
