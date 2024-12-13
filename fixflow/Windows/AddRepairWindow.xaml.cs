using fixflow.Model;
using fixflow.Utility;
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
using System.Windows.Shapes;

namespace fixflow.Windows
{
    /// <summary>
    /// Interaction logic for AddRepairWindow.xaml
    /// </summary>
    public partial class AddRepairWindow : Window
    {
        private Ticket _ticket;
        public AddRepairWindow(Ticket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
            repairName_TextBox.Focus();
            
        }

        private async void addRepair_Button_Click(object sender, RoutedEventArgs e)
        {
            var intParse = int.TryParse(price_TextBox.Text, out int p);
            if (!intParse)
            {
                MessageBox.Show(Rm.Get("wrong_price"), Rm.Get("error"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = await ApiClient.TicketRepair.Post(_ticket.Id, repairName_TextBox.Text, p);
            if (result)
            {
                this.Close();
            }
        }

        private void price_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addRepair_Button_Click(null, null);
            }
        }

        private void price_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Helper.IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void repairs_Asb_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
