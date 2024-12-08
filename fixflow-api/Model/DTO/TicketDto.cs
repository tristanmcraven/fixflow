using System.ComponentModel.DataAnnotations;

namespace fixflow_api.Model.DTO
{
    public class TicketDto
    {
        [Required]
        public uint DeviceBrandId { get; set; }

        [Required]
        public uint DeviceModelId { get; set; }

        public string? ClientFullname { get; set; }

        public string? ClientPhoneNumber { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public string? Note { get; set; }

        public string? Description { get; set; }

        public class ChangeName
        {
            public uint TicketId { get; set; }
            public string Name { get; set; }
        }

        public class ChangePhone
        {
            public uint TicketId { get; set; }
            public string Phone { get; set; }
        }
        public class ChangeDeviceBrand
        {
            public uint TicketId { get; set; }
            public uint DeviceBrandId { get; set; }
        }
        public class ChangeDeviceModel
        {
            public uint TicketId { get; set; }
            public uint DeviceModelId { get; set; }
        }
    }
}
