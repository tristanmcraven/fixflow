using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model.DTO
{
    public class TicketDto
    {
        [Required]
        public Guid DeviceBrandId { get; set; }

        [Required]
        public Guid DeviceModelId { get; set; }

        [Required]
        public Guid DeviceTypeId { get; set; }

        public string? ClientFullname { get; set; }

        public string? ClientPhoneNumber { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public string? Note { get; set; }

        public string? Description { get; set; }

        public class ChangeName
        {
            public Guid TicketId { get; set; }
            public string Name { get; set; }
        }

        public class ChangePhone
        {
            public Guid TicketId { get; set; }
            public string Phone { get; set; }
        }
        public class ChangeDeviceBrand
        {
            public Guid TicketId { get; set; }
            public Guid DeviceBrandId { get; set; }
        }
        public class ChangeDeviceModel
        {
            public Guid TicketId { get; set; }
            public Guid DeviceModelId { get; set; }
        }
        public class ChangeDeviceType
        {
            public Guid TicketId { get; set; }
            public Guid DeviceTypeId { get; set; }
        }
    }
}
