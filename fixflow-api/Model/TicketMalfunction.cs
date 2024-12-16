using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class TicketMalfunction
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketMalfunction()
    {
    }

    public TicketMalfunction(Guid ticketId, string name)
    {
        Guid = Guid.NewGuid();
        TicketGuid = ticketId;
        Name = name;
    }

}
