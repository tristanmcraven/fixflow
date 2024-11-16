using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketMalfunction
{
    public uint Id { get; set; }

    public uint TicketId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
