using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketRepair
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public string Repair { get; set; } = null!;

    public int? Price { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
