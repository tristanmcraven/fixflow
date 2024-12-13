using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class Repair
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TicketRepair> TicketRepairs { get; set; } = new List<TicketRepair>();

    public Repair()
    {
    }

    public Repair(string name)
    {
        Name = name;
    }
}
