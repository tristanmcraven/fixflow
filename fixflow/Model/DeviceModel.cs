using System;
using System.Collections.Generic;

namespace fixflow.Model;

public partial class DeviceModel
{
    public Guid Guid { get; set; }

    public Guid DeviceBrandGuid { get; set; }

    public string Name { get; set; } = null!;

    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public DeviceModel()
    {
    }

    public DeviceModel(Guid deviceBrandGuid, string name)
    {
        Guid = Guid.NewGuid();
        DeviceBrandGuid = deviceBrandGuid;
        Name = name;
    }
}
