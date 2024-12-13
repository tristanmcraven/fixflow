﻿using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class Repair
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TicketRepair> TicketRepairs { get; set; } = new List<TicketRepair>();
}
