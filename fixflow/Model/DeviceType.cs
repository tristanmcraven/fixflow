using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class DeviceType
{
    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
