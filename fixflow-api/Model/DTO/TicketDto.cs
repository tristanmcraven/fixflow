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
    }
}
