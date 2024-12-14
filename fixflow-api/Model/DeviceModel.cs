using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace fixflow_api.Model;

public partial class DeviceModel
{
    public Guid Guid { get; set; }

    public Guid DeviceBrandGuid { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public DeviceModel()
    {
    }

    public DeviceModel(Guid deviceBrandId, string name)
    {
        Guid = Guid.NewGuid();
        DeviceBrandGuid = deviceBrandId;
        Name = name;
    }
}
