using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class DeviceModel
{
    public uint Id { get; set; }

    public uint DeviceBrandId { get; set; }

    public string Name { get; set; } = null!;

    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
