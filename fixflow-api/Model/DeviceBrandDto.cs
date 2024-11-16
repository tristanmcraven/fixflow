using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model
{
    public class DeviceBrandDto
    {
        [Required]
        public string Name { get; set; }
    }
}
