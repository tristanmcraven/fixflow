using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class TicketRepair
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public uint RepairId { get; set; }

    public int? Price { get; set; }

    public virtual Repair Repair { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketRepair()
    {
    }

    public TicketRepair(uint ticketId, uint repairId, int? price)
    {
        TicketId = ticketId;
        RepairId = repairId;
        Price = price;
    }
}
