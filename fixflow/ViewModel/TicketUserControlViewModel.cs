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
        public Ticket Ticket { get; set; }

        public Status Status { get; set; }

        public TicketUserControlViewModel(Ticket ticket, Status ticketstatus)
        {
            Ticket = ticket;
            Status = ticketstatus;
        }
    }
}
