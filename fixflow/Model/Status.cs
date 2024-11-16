using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class Status
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TicketStatus> TicketStatuses { get; set; } = new List<TicketStatus>();
}
