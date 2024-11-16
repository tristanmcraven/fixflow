using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model.DTO
{
    public class DeviceModelDTO
    {
        [Required]
        public uint DeviceBrandId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
