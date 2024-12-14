﻿using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class TicketRepair
{
    public Guid Guid { get; set; }

    public Guid TicketGuid { get; set; }

    public Guid RepairGuid { get; set; }

    public uint Price { get; set; }

    public virtual Repair Repair { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
