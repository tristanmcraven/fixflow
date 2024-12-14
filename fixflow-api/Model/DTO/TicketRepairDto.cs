namespace fixflow_api.Model.DTO
{
    public class TicketRepairDto
    {
        public Guid TicketId { get; set; }
        public Guid RepairId { get; set; }
        public uint Price { get; set; }
    }
}
