﻿using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model.DTO
{
    public class DeviceModelDTO
    {
        [Required]
        public Guid DeviceBrandId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
