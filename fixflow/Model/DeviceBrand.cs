using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class DeviceBrand
{
    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
