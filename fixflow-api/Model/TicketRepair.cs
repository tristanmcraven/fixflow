using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class TicketRepair
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public Guid RepairGuid { get; set; }

    public uint Price { get; set; }

    public virtual Repair Repair { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketRepair()
    {
    }

    public TicketRepair(Guid ticketId, Guid repairId, uint price)
    {
        Guid = Guid.NewGuid();
        TicketGuid = ticketId;
        RepairGuid = repairId;
        Price = price;
    }
}
