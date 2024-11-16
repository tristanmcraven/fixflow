using System;
using System.Collections.Generic;

namespace fixflow_api.Model;

public partial class DeviceBrand
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public DeviceBrand()
    {
    }

    public DeviceBrand(string name)
    {
        Name = name;
    }
}
