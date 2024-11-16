using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model.DTO
{
    public class DeviceBrandDto
    {
        [Required]
        public string Name { get; set; }
    }
}
