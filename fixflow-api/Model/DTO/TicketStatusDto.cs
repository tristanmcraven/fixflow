namespace fixflow_api.Model.DTO
{
    public class TicketStatusDto
    {
        public Guid? Guid { get; set; }
        public Guid TicketId { get; set; }
        public Guid StatusId { get; set; }
    }
}
