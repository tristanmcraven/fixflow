using fixflow.Model;
using fixflow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fixflow.UserControls
{
    /// <summary>
    /// Interaction logic for TicketUserControl.xaml
    /// </summary>
    public partial class TicketUserControl : UserControl
    {
        public TicketUserControl(Ticket ticket, TicketStatus ticketstatus)
        {
            InitializeComponent();
            this.DataContext = new TicketUserControlViewModel(ticket, ticketstatus);
        }
    }
}
