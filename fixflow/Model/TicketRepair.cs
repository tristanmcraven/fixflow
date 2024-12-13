using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketRepair
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public uint RepairId { get; set; }

    public int? Price { get; set; }

    public virtual Repair Repair { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
