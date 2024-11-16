using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace fixflow_api.Model;

public partial class DeviceModel
{
    public uint Id { get; set; }

    public uint DeviceBrandId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual DeviceBrand DeviceBrand { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public DeviceModel()
    {
    }

    public DeviceModel(uint deviceBrandId, string name)
    {
        DeviceBrandId = deviceBrandId;
        Name = name;
    }
}
