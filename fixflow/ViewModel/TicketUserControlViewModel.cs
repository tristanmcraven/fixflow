using fixflow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.ViewModel
{
    class TicketUserControlViewModel
    {
        public int NumericId { get; set; }
        public Ticket Ticket { get; set; }

        public Status Status { get; set; }

        public TicketUserControlViewModel(int numericId, Ticket ticket, Status ticketstatus)
        {
            NumericId = numericId;
            Ticket = ticket;
            Status = ticketstatus;
        }
    }
}
