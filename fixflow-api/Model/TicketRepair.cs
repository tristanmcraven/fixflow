using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class TicketRepair
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public string Repair { get; set; } = null!;

    public int? Price { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public TicketRepair()
    {
    }

    public TicketRepair(uint ticketId, string repair, int? price)
    {
        TicketId = ticketId;
        Repair = repair;
        Price = price;
    }
}
